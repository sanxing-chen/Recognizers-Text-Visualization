/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/prism/prism.d.ts" />
var RecognizeResult = /** @class */ (function () {
    function RecognizeResult(id, content, language) {
        this.id = id;
        this.content = content;
        this.language = language;
    }
    return RecognizeResult;
}());
function setJson() {
    recognizeResults = JSON.parse($("#viewbagTextJson").val())
        .map(function (e) { return new RecognizeResult(e.Id, e.Content, e.Language); });
}
function selectText(id) {
    $("#textArea").text(recognizeResults[id - 1].content);
    currentTextId = id;
}
function getRocognizeResults() {
    function highlightText(results) {
        var Item = /** @class */ (function () {
            function Item() {
            }
            return Item;
        }());
        var positions = new Array();
        for (var _i = 0, results_1 = results; _i < results_1.length; _i++) {
            var result = results_1[_i];
            positions.push({ pos: result.Start, type: true });
            positions.push({ pos: result.End, type: false });
        }
        positions.sort(function (a, b) {
            if (a.pos !== b.pos)
                return a.pos - b.pos;
            else if (a.type && !b.type)
                return -1;
            else
                return 1;
        });
        var text = $("#textArea").text();
        var bias = 0;
        for (var _a = 0, positions_1 = positions; _a < positions_1.length; _a++) {
            var item = positions_1[_a];
            if (item.type) {
                text = text.substr(0, item.pos + bias) + "<mark>" + text.substr(item.pos + bias);
                bias += 6;
            }
            else {
                text = text.substr(0, item.pos + 1 + bias) + "</mark>" + text.substr(item.pos + 1 + bias);
                bias += 7;
            }
        }
        $("#textArea").html(text);
    }
    $.post({ url: "/api/recognize", data: { id: currentTextId, checkedModelString: JSON.stringify(op) } })
        .done(function (msg) {
        highlightText(msg);
        $("#jsonArea").html("<code>" + JSON.stringify(msg, null, 2) + "</code>");
        Prism.highlightAll();
    });
}
if ($("#viewbagTextJson").length) {
    var recognizeResults;
    var op;
    var currentTextId;
    $(".checkbox").change(function () {
        op = $(":checked").toArray().map(function (e) { return parseInt($(e).val()); });
    });
    setJson();
}
//# sourceMappingURL=app.js.map