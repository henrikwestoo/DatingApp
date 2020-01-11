window.addEventListener('load', () => {

});


function removePost(contactId, category) {

    $.ajax({
        type: "POST",
        url: "/contact/editcategory",
        data: { contactId: contactId, category: category  },
    });

}