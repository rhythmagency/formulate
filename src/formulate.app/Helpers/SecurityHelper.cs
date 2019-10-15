namespace formulate.app.Helpers
{

    // Namespaces.
    using System.Web;
    using System.Web.Helpers;


    /// <summary>
    /// Assists with operations related to security.
    /// </summary>
    public class SecurityHelper
    {

        #region Methods

        /// <summary>
        /// Generates an anti-forgery token to be sent with a form submission using the name
        /// "__RequestVerificationToken".
        /// </summary>
        /// <returns>
        /// The token.
        /// </returns>
        /// <remarks>
        /// This will also read in and set the appropriate cookie tokens for the current request/response.
        /// </remarks>
        public static string GenerateAntiForgeryToken()
        {

            // Variables.
            var context = HttpContext.Current;
            var request = context.Request;
            var isSecure = request.IsSecureConnection;
            var response = context.Response;
            var cookie = request.Cookies[AntiForgeryConfig.CookieName];
            var oldCookieToken = cookie?.Value;
            var cookieToken = default(string);
            var formToken = default(string);


            // Get tokens (a cookie token and a form token).
            AntiForgery.GetTokens(oldCookieToken, out cookieToken, out formToken);


            // If a new cookie token was generated, set it in the response.
            if (!string.IsNullOrEmpty(cookieToken))
            {
                cookie = cookie ?? new HttpCookie(AntiForgeryConfig.CookieName);
                cookie.Value = cookieToken;
                cookie.Secure = isSecure;
                cookie.HttpOnly = true;
                response.Cookies.Add(cookie);
            }


            // Return the form token.
            return formToken;

        }

        #endregion

    }

}