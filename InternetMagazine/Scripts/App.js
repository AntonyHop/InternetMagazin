
$(function () {

    setTimeout(function () {
        $(".Errors").hide(300);
    },4000);

    $(".container .pItem").click(function () {
        window.location = "/home/ProductItem/" + $(this).data("item");
    })

    $(".submiThis").click(function () {
        var send = true;
        $(".ValidThisForm input[type=text], .ValidThisForm input[type=password], .ValidThisForm input[type=tel]").each(function () {
            if ($(this).val() == "") {
                $(this).addClass("is-invalid");
                send = false;
            } else {
                $(this).removeClass("is-invalid");
            }
        });
        console.log(send);
        if (send) {
            $(".ValidThisForm").submit();
        }
    })
})