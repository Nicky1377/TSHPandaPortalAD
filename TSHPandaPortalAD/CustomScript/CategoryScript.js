
    function modalloader(name, id) {

        document.getElementById("off").innerHTML = name;
        $("#modeloff").val(id);

        document.getElementById("delete").innerHTML = name;
        $("#modeldelete").val(id);

        document.getElementById("on").innerHTML = name;
        $("#modelon").val(id);

        document.getElementById("edit").innerHTML = name;
        $("#modeledit").val(id);

    }
 

 
    $(document).ready(function () {
        alert("helooooo");

      
        document.getElementById("show_newtype").val = "heloooo";
        // $('#show_newtype').load("https://localhost:44370/MS/New_type");
        // $('#show_tab').load("https://localhost:44370/MS/table_Type");
    });


function savePics(action) {
    var myObject = {
        firstName: "#model",
        lastName: action,
        fullName: function () {
            return this.firstName + "" + this.lastName;
        }
    }
    var x = myObject.fullName();
    var id = $(x).val();


    var AddressToSend = "Type_Switch";
    var PostJson = {
        'action': action,
        'id': id
    };
    $.ajax({
        url: AddressToSend,
        type: "post",
        data: JSON.stringify(PostJson),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#ERRAlertNew").hide(300);
            $("#SuccesAlertNew").show(300);
            $('#show_tab').load("https://localhost:44370/MS/table_Type");
            $('#show_newtype').load("https://localhost:44370/MS/New_type");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
    return false;
}
