/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/prism/prism.d.ts" />

class RecognizeResult {
    id: number;
    content: string;
    language: number;

    constructor(id: number, content: string, language: number) {
        this.id = id;
        this.content = content;
        this.language = language;
    }

}

function setJson() {
    recognizeResults = JSON.parse($("#viewbagTextJson").val())
        .map((e: any) => new RecognizeResult(e.Id, e.Content, e.Language));
}

function selectText(id: number) {
    $("#textArea").text(recognizeResults[id - 1].content);
    currentTextId = id;
}

function getRocognizeResults() {
    function highlightText(results: Array<any>) {
        class Item {
            pos: number;
            type: boolean;
        }

        const positions = new Array<Item>();

        for (let result of results) {
            positions.push({ pos: result.Start, type: true });
            positions.push({ pos: result.End, type: false });
        }
        positions.sort((a, b) => {
            if (a.pos !== b.pos) return a.pos - b.pos;
            else if (a.type && !b.type) return -1;
            else return 1;
        });

        let text = $("#textArea").text();
        let bias = 0;
        for (let item of positions) {
            if (item.type) {
                text = text.substr(0, item.pos + bias) + "<mark>" + text.substr(item.pos + bias);
                bias += 6;
            } else {
                text = text.substr(0, item.pos + 1 + bias) + "</mark>" + text.substr(item.pos + 1 + bias);
                bias += 7;
            }
        }
        $("#textArea").html(text);
    }

    $.post({ url: "/api/recognize", data: { id: currentTextId, checkedModelString: JSON.stringify(op) } })
        .done(msg => {
            highlightText(msg);
            $("#jsonArea").html(`<code>${JSON.stringify(msg, null, 2)}</code>`);
            Prism.highlightAll();
        });
}

if ($("#viewbagTextJson").length) {
    var recognizeResults: RecognizeResult[];
    var op: number[];
    var currentTextId: number;

    $(".checkbox").change(() => {
        op = $(":checked").toArray().map(e => parseInt($(e).val()));
    });

    setJson();
}