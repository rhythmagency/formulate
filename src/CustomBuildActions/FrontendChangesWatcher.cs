﻿namespace CustomBuildActions;

using System;
using System.Threading.Tasks;

/// <summary>
/// Watches for frontend changes and executes a task when
/// that occurs.
/// </summary>
internal class FrontendChangesWatcher
{
    /// <summary>
    /// Watches for file system changes.
    /// </summary>
    /// <remarks>
    /// This is static so it doesn't get garbage collected.
    /// </remarks>
    private static FileSystemWatcher? Watcher { get; set; }

    /// <summary>
    /// A lock object to avoid collisions between parallel watch events.
    /// </summary>
    private static object WatchLock { get; set; } = new object();

    /// <summary>
    /// Should the task be executed?
    /// </summary>
    private static bool ShouldExecute { get; set; }

    /// <summary>
    /// The number of times a task has been executed.
    /// </summary>
    public static long ExecutionCount { get; set; }

    /// <summary>
    /// Adds a file system watcher to execute a task any time the source
    /// files change.
    /// </summary>
    /// <param name="task">
    /// The task to execute when a source file changes. Each time it executes,
    /// it will be passed a number indicating the run count.
    /// </param>
    public static void AddWatcher(Action<long> task)
    {
        var source = PathUtils.NormalizePath("../Formulate.BackOffice.StaticAssets/App_Plugins/FormulateBackOffice");
        Console.WriteLine($"Adding watcher for: {source}");
        Watcher = new FileSystemWatcher
        {
            Path = source,
            NotifyFilter = NotifyFilters.LastWrite,
            Filter = "*.*",
            IncludeSubdirectories = true,
        };
        Watcher.Changed += new FileSystemEventHandler((_, _) =>
        {
            ScheduleTaskExecution(task);
        });
        Watcher.EnableRaisingEvents = true;
    }

    /// <summary>
    /// Waits a moment and then performs the task (if another hasn't already
    /// been performed in that time). This acts as a way of
    /// debouncing/throttling.
    /// </summary>
    /// <param name="task">
    /// The task to execute.
    /// </param>
    private static void ScheduleTaskExecution(Action<long> task)
    {
        Task.Factory.StartNew(() =>
        {
            ShouldExecute = true;
            lock (WatchLock)
            {
                Thread.Sleep(TimeSpan.FromSeconds(.25));
                if (ShouldExecute)
                {
                    // Pause watching for further events (in case this task
                    // generates new files).
                    Watcher.EnableRaisingEvents = false;

                    // Variables.
                    ShouldExecute = false;
                    ExecutionCount++;

                    // Execute the task.
                    task(ExecutionCount);

                    // Give the user instructions to stop the watcher.
                    Console.WriteLine(Constants.PressEnterToStop);

                    // Wait a moment.
                    Thread.Sleep(TimeSpan.FromSeconds(.25));

                    // Variables.
                    ShouldExecute = false;

                    // Resume watching for file system changes.
                    Watcher.EnableRaisingEvents = true;
                }
            }
        });
    }
}