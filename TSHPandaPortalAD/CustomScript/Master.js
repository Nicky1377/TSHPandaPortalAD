var KTAppOptions = {
    "colors": {
        "state": {
            "brand": "#5d78ff",
            "dark": "#282a3c",
            "light": "#ffffff",
            "primary": "#5867dd",
            "success": "#34bfa3",
            "info": "#36a3f7",
            "warning": "#ffb822",
            "danger": "#fd3995"
        },
        "base": {
            "label": [
                "#c5cbe3",
                "#a1a8c3",
                "#3d4465",
                "#3e4466"
            ],
            "shape": [
                "#f0f3ff",
                "#d9dffa",
                "#afb4d4",
                "#646c9a"
            ]
        }
    }
};

function SubmitedType(action, id) {
    if (action === 'new') {
        var message = document.getElementById("inputNew").value;
        if ($("#inputNew").val() !== '') {


            var AddressToSend = "/Panel/Add_Update_Type";

            $.ajax({
                url: AddressToSend,
                type: "get",
                data: { "HasChang": false, "text": message,"Id":id},
                success: function (response) {
                    $("#ERRAlertNew").hide(300);
                    $("#SuccesAlertNew").show(300);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
            return false;
        } else {
            $("#ERRAlertNew").show(300);
        }


    } else if (action === 'edit') {

        if ($("#inputEdit").val() !== '') {

            var message = document.getElementById("inputEdit").value;
            var AddressToSend = "/Panel/Add_Update_Type";

            $.ajax({
                url: AddressToSend,
                type: "get",
                data: { "HasChang": true, "text": message, "Id": id },
                success: function (response) {
                    $("#ERRAlertEdit").hide(300);
                    $("#SuccesAlertEdit").show(300);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
            return false;
        } else {
            $("#ERRAlertEdit").show(300);
        }
    }
}


function ModallFunc(action,id) {
    if (action === 'delete') {
        $.ajax({
            url: "/Panel/Delete",
            type: "get",
            data: { "id": id },
            success: function (response) {
               
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
        return false;

    } else if (action === 'disable') {
        $.ajax({
            url:"/Panel/Disable",
            type: "get",
            data: {  "id": id },
            success: function (response) {
               
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
        return false;
    } 

}
