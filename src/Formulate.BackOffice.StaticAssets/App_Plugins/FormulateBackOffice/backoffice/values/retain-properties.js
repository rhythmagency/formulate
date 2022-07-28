/**
 * Stores the specified properties on the current or specified object.
 * @param {any} properties The object containing the properties to store.
 * @param {any} instance Optional. The object to retain properties on.
 */
function retainProperties (properties, instance) {
    instance = instance || this;
    console.log({instance});
    for (const [key, value] of Object.entries(properties)) {
        instance[key] = value;
    }
}

// Register the value.
angular.module('umbraco').value('retainProperties', retainProperties);