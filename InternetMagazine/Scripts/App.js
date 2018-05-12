
$(function () {



    setTimeout(function () {
        $(".Errors").hide(300);
    }, 4000);

    $(".container .pItem").click(function () {
        window.location = "/home/ProductItem/" + $(this).data("item");
    })

    $(".submiThis").click(function () {
        var send = true;
        $(".ValidThisForm input[type=text], .ValidThisForm input[type=password], .ValidThisForm input[type=tel], .ValidThisForm input[type=number]").each(function () {
            if ($(this).val() == "") {
                $(this).addClass("is-invalid");
                send = false;
            } else {
                $(this).removeClass("is-invalid");
            }
        });
       
        if (send) {
            $(".ValidThisForm").submit();
        }
    })

    $(".add").click(function () { 
        swal("Введите название катрегории", {
            content: "input",
        }).then((res) => {

            var CaegoryModel = {
                id: 0,
                Name: res,
                Mode: "add"
            }
            if (res != "") {
                $.ajax({
                    url: '/Category/Categories',
                    type: 'POST',
                    data: CaegoryModel,
                    success: function (result) {
                        swal("Прекрасно", "Категория добавлена", "success");
                        setTimeout(function () {
                            location.reload();
                        },500) 
                    }
                });
            } else {
                swal("О нет! ", "Категория не добавлена", "error");
            }
        });
    });

    $(".edit").click(function () {
     
        var add = $(this).parent().siblings(".name");
        swal("Введите название катрегории", {
            closeOnClickOutside: false,
            closeOnEsc: false,
            content: {
                element: "input",
                attributes: {
                    value: $(add).text(),
                },
            },
        }).then((res) => {

            var CaegoryModel = {
                id: $(this).data("id"),
                Name: res,
                Mode: "edit"
            }
            if (res != '') {
                $.ajax({
                    url: '/Category/Categories',
                    type: 'POST',
                    data: CaegoryModel,
                    success: function (result) {
                        add.text(res);
                        swal("Прекрасно", "Категория изменена", "success");
                    }
                });
            }
        });
    });
    $(".delete").click(function () {
        var dell = $(this).parent().parent();
        swal({
            title: "Удалить текущую категорию?",
            text: "Если вы удалите категорию то все товары останутся без категории",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                var CaegoryModel = {
                    id: $(this).data("id"),
                    Name: "",
                    Mode: "delete"
                }
                if (willDelete) {
                    $.ajax({
                        url: '/Category/Categories',
                        type: 'POST',
                        data: CaegoryModel,
                        success: function (result) {
                            if (result != "no") {
                                $(dell).hide();
                                swal("Прекрасно", "Категория удалена", "success");
                            } else {
                                swal("Gлохо", "Категория не удалена", "error");
                            }

                        }
                    });

                }
            });
    });

    $("table .deleteProduct").click(function () {
        elem = $(this);
        swal({
            title: "Удалить текущий товар?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        }).then((willDelete) => {
            if (willDelete) {
                $.get("/Category/RemoveProduct/" + $(this).data("id"), function (data) {
                    if (data == "done") {
                        swal("Отлично", "Товар удален", "success");
                        $(elem).parent().parent().hide();
                    } else {
                        swal("Товар не удален", data, "error");
                    }
                })
            }

        });
    });

    $("table .deleteUser").click(function () {
        elem = $(this);
        swal({
            title: "Удалить текущего пользователя?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        }).then((willDelete) => {
            if (willDelete) {
                $.get("/User/Delete/" + $(this).data("id"), function (data) {
                    if (data == "done") {
                        swal("Отлично", "Пользователь удален", "success");
                        $(elem).parent().parent().hide();
                    } else {
                        swal("Пользователь не удален", data, "error");
                    }
                })
            }

        });
    });
});