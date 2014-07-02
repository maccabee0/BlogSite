var Paginator = function (list, size) {
    var self = this;

    self.pageSize = size;
    self.list = ko.observableArray(list);
    self.pageIndex = ko.observable(0);

    self.pagedList = ko.computed(function() {
        var start = self.pageIndex() * self.pageSize;
        return self.list().slice(start, start + self.pageSize);
    });

    self.maxPageIndex = ko.computed(function() {
        var cil = Math.ceil(self.list().length / self.pageSize);
        if (cil > 0) return cil - 1;
        return cil;
    });

    self.allPages = ko.computed(function () {
        var pages = [], i;
        for (i = 0; i <= self.maxPageIndex(); i++) {
            pages.push({ pageNumber: (i + 1) });
        }
        return pages;
    });

    self.firstPage = function () {
        self.pageIndex(0);
    };

    self.lastPage = function () {
        self.pageIndex(self.maxPageIndex());
    };

    self.previousPage = function () {
        self.pageIndex(self.pageIndex() - 1);
    };

    self.nextPage = function () {
        self.pageIndex(self.pageIndex() + 1);
    };

    self.moveToPage = function (index) {
        self.pageIndex(index);
    };
};