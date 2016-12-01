$(document)
        .ready(function () {
            var token = [];
            var engine;
            $.get('@Url.Action("AutocompleteSearch", "Home")',
                function (data) {
                    $.each(data,
                        function (i, v) {
                            token.push({ value: v });
                        });
                });

            console.log(token);

        });