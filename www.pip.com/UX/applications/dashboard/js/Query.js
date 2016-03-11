/*global Ektron,alert,console*/
define([],
    function () {
        'use strict';

        function Query(text, filters, orderBy, orderAscending) {
            var me = this;

            me.text = text;
            me.filters = [];
            me.orderBy = {};
            me.orderAscending = false;
            me.pageNumber = 1;
            me.pageSize = 10;

            if (typeof filters !== "undefined") {
                me.filters = filters;
            }

            if (typeof orderBy !== "undefined") {
                me.orderBy = orderBy;
            }

            if (typeof orderAscending !== "undefined") {
                me.orderAscending = orderAscending;
            }

            // translate orderBy
            switch (me.orderBy) {
                case 'TitleAscending':
                    me.orderBy = 'title';
                    me.orderAscending = true;
                    break;

                case 'TitleDescending':
                    me.orderBy = 'title';
                    me.orderAscending = false;
                    break;

                case 'DateModifiedAscending':
                    me.orderBy = 'datemodified';
                    me.orderAscending = true;
                    break;

                case 'DateModifiedDescending':
                    me.orderBy = 'datemodified';
                    me.orderAscending = false;
                    break;

                case 'MostRelevant':
                    me.orderBy = 'rank';
                    me.orderAscending = false;
                    break;

                case 'StartDateAscending':
                    me.orderBy = 'golivedate';
                    me.orderAscending = true;
                    break;

                case 'EndDateAscending':
                    me.orderBy = 'expirydate';
                    me.orderAscending = true;
                    break;

                case 'IDAscending':
                    me.orderBy = 'id';
                    me.orderAscending = true;
                    break;

                case 'SizeDescending':
                    me.orderBy = 'size';
                    me.orderAscending = false;
                    break;

                case 'FolderNameAscending':
                    me.orderBy = 'foldername';
                    me.orderAscending = true;
                    break;

                case 'LanguageAscending':
                    me.orderBy = 'language';
                    me.orderAscending = true;
                    break;

                default:
                    // no translation, use passed-in values
                    break;
            };

            me.clone = function () {
                return Query.clone(me);
            };

            me.isEmpty = function () {
                return me.text === '' && me.filters.length === 0;
            };
        }

        Query.clone = function (query) {
            var copy = JSON.parse(JSON.stringify(query));

            return new Query(copy.text, copy.filters, copy.orderBy, copy.orderAscending);
        };

        return Query;
    }
);
