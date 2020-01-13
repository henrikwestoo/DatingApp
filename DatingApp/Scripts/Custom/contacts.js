function changeCategory(contactId, category) {


    var category = $("#" + contactId + "-dropdown option:selected").val();

    $.ajax({
        type: "POST",
        url: "/contact/editcategory",
        data: { contactId: contactId, category: category  },
    });

}