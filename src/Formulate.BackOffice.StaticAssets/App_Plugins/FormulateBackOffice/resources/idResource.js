(function () {
    // Variables.
    var app = angular.module("umbraco");

    app.factory("formulateIds", function () {
        const sanitizeRegex = /-/g;

        return {
            compare: compare,
            sanitize: sanitize,
        };

        function compare(a, b) {
            if (a === b) {
                return true;
            }

            if (typeof (a) === 'undefined') {
                return false;
            }

            if (typeof (b) === 'undefined') {
                return false;
            }

            return sanitize(a) === sanitize(b);
        }

        function sanitize(id) {
            if (!id) {
                return '';
            }

            return id.trim().toLowerCase().replace(sanitizeRegex, '');
        }
    });
})();