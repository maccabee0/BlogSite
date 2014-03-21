$(function () {
    //Post Comment with AJAX
    $("#buttonId").on("click", function () {
        var data = JSON.stringify({
            BlogId: $("#BlogId").val(),
            Text: $('#text').val()
        });
        $.ajax({
            "data": data,
            "contentType": "application/json; charset=utf-8",
            "url": $('#SubmitCommentUrl').val(),
            "type": "POST",
            "success": function (e) {
                $('#comments').html(e);
            },
            "error": function (jqXHR, status, error) {
                console.log(jqXHR.responseText);
                console.log("status:", status, "error:", error);
            }
        });
    });

    //AJAX Change viewed blogs
    $('.pagination > ul > li > a').on('click', function () {
        var data = JSON.stringify({
            page: $(this).attr('value')
        });
        $.ajax({
            "data": data,
            "contentType": "application/json; charset=utf-8",
            "url": $('#pagerUrl').val(),
            "type": "POST",
            "success": function (e) {
                $('#blog').html(e);
            },
            "error": function (jqXHR, status, error) {
                console.log("this: " + this + " status: " + status + " error: " + error);
            }
        });
        return;
    });

    $.getJSON("api/blogs/", function (data) {
        var dataForView = [];
        $.each(data, function (key, val) {
            dataForView.push({
                ID: val.ID,
                Subject: val.Subject,
                Description: val.Description,
                Author: val.Author,
                PostDate: val.PostDate
            });
        });
        ko.applyBindings(new ListViewModel(dataForView, $('#blogPage').val(), $('#userPage').val()));
    });

    //$('#blogTextSubmit').on('click', function () {
    //    var data = $('#blogText').val();
    //    $.post($('#inputToString').val(), function() {
    //        alert('success');
    //    });
    //$.ajax({
    //    "data": data,
    //    "contentType": "application/json; charset=utf-8",
    //    "url": $('#inputToString').val(),
    //    "type": "POST",
    //    "success": function (e) {
    //        $('#Text').html(e);
    //    },
    //    "error": function (jqXHR, status, error) {
    //        console.log("status: " + status + " error: " + error);
    //    }
    //});
    //});
});


var ListViewModel = function (initialData, blogPage, userPage) {
    var self = this;
    //window.viewModel = self;

    self.list = ko.observableArray(initialData);
    self.pageSize = ko.observable(6);
    self.pageIndex = ko.observable(0);
    self.blogUrl = blogPage + '/';
    self.userUrl = userPage + '?username=';
    self.pagedList = ko.dependentObservable(function () {
        var size = self.pageSize();
        var start = self.pageIndex() * size;
        return self.list.slice(start, start + size);
    });

    self.maxPageIndex = ko.dependentObservable(function () {
        return Math.ceil(self.list().length / self.pageSize()) - 1;
    });

    self.previousPage = function () {
        if (self.pageIndex() > 0) {
            self.pageIndex(self.pageIndex() - 1);
        }
    };

    self.nextPage = function () {
        if (self.pageIndex() < self.maxPageIndex()) {
            self.pageIndex(self.pageIndex() + 1);
        }
    };

    self.allPages = ko.computed(function () {
        var pages = [];
        for (var i = 0; i <= self.maxPageIndex() ; i++) {
            pages.push({ pageNumber: (i + 1) });
        }
        return pages;
    });

    self.moveToPage = function (index) {
        self.pageIndex(index);
    };
};

//ko.bindingHandlers.dateString = {
//    init: function (element, valueAccessor) {
//        var jsonDate = ko.utils.unwrapObservable(valueAccessor());
//        var value = new Date(parseInt(jsonDate.substr(6)));
//        var date = value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getYear();
//        $(element).text(date);
//    }
//}