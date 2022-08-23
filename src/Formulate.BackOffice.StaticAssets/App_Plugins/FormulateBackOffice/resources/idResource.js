(function () {
    // Variables.
    var app = angular.module("umbraco");

    app.factory("formulateIds", function () {
        const sanitizeRegex = /-/g;
        const sanitizeZerosRegex = /0/g;

        return {
            compare: compare,
            sanitize: sanitize,
            isEmpty: isEmptyOrNew,
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

        function isEmptyOrNew(id) {
            if (!id) {
                return true;
            }

            if (!id.length === 0) {
                return true;
            }

            const sanitizedId = sanitize(id);

            return sanitizedId.replace(sanitizeZerosRegex, '').length === 0;
        }
    });
})();