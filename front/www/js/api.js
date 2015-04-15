var API = function()
{
    var URI_ROOT = 'http://setest.plexusdev.ru/API/';

    if (window.API == undefined || window.API == null)
    {
        window.API = {};
    }

    if (window.API.Events == undefined || window.API.Events == null) {
        window.API.Events =
        {
            ON_LOAD_USERS: 'event_load_users',
            ON_LOAD_INVOICES: 'event_load_invoices'
        };
    }

    var Events =  window.API.Events;

    function LoadUsers()
    {
        $.ajax({
            url: URI_ROOT+'GetUsers.php',
            type : 'GET',
            dataType : 'json',
            success: function (data, textStatus)
            {
                window.Users = [];

                $.each(data, function(key, val)
                {
                    window.Users.push(val);
                });

                $(API().Events).triggerHandler(
                    {
                        type: Events.ON_LOAD_USERS,
                        data: window.Users
                    }
                )
            }
        });
    }

    function LoadInvoices()
    {
        $.ajax({
            url: URI_ROOT+'GetInvoices.php',
            type : 'GET',
            dataType : 'json',
            success: function (data, textStatus)
            {
                window.Invoices = [];

                $.each(data, function(key, val)
                {
                    window.Invoices.push(val);
                });

                $(API().Events).triggerHandler(
                    {
                        type: Events.ON_LOAD_INVOICES,
                        data: window.Invoices
                    }
                )
            }
        });
    }

    function AddInvoice(idSender, idRecipient, count, price, itemName)
    {
        $.ajax({
            url: URI_ROOT+'AddInvoice.php',
            data :
            {
                id_sender: idSender,
                id_recipient: idRecipient,
                item_name: itemName,
                item_count: count,
                item_price: price
            },
            type : 'GET',
            dataType : 'text',
            success: function (data, textStatus)
            {
            }
        });
    }

    return {
        Events: Events,
        Users: window.Users,

        LoadUsers: LoadUsers,
        LoadInvoices: LoadInvoices,
        AddInvoice: AddInvoice
    }
};

var Content = function()
{
    var UsersList = function()
    {
        var GRID_SHEMA =
            "<div class=\"row\">"+
            "<div class=\"col-md-4\">#Col1#</div>"+
            "<div class=\"col-md-4\">#Col2#</div>"+
            "<div class=\"col-md-4\">#Col3#</div>"+
            "</div>";

        var PHOTO_SHEMA =
            "<div class=\"row\">"+
                "<div class=\"col-md-4 users-list-object\">"+
                    "<div id=\"thumbnail-#UserID#\" class=\"thumbnail\">"+
                        "<img src=\"#PhotoSrc#\" " +
                        "alt=\"#PhotoAlt#\"" +
                        "onclick=\"Content().OnPhotoClick(#UserID#)\">"+
                            "<div class=\"caption\">"+
                                "<h3>Name: #UserName#</h3>"+
                                "<p>ID: #UserID#</p>"+
                            "</div>"+
                    "</div>"+
                "</div>"+
            "</div>";

        var GetThumbnail = function (user1, user2, user3)
        {
            var result = "";

            var grid = GRID_SHEMA;

            var th = '';
            if (user1 != null) {
                th = PHOTO_SHEMA;
                th = th.replace("#PhotoSrc#", user1.user_photo_uri);
                th = th.replace("#PhotoAlt#", user1.name);
                th = th.replace("#UserName#", user1.name);
                th = th.replace("#UserID#", user1.id);
                th = th.replace("#UserID#", user1.id);
                th = th.replace("#UserID#", user1.id);
                grid = grid.replace("#Col1#", th);
            }
            else
            {
                grid = grid.replace("#Col1#", '');
            }

            if (user1 != null) {
                th = PHOTO_SHEMA;
                th = th.replace("#PhotoSrc#", user2.user_photo_uri);
                th = th.replace("#PhotoAlt#", user2.name);
                th = th.replace("#UserName#", user2.name);
                th = th.replace("#UserID#", user2.id);
                th = th.replace("#UserID#", user2.id);
                th = th.replace("#UserID#", user2.id);
                grid = grid.replace("#Col2#", th);
            }
            else
            {
                grid = grid.replace("#Col2#", '');
            }

            if (user1 != null) {
                th = PHOTO_SHEMA;
                th = th.replace("#PhotoSrc#", user3.user_photo_uri);
                th = th.replace("#PhotoAlt#", user3.name);
                th = th.replace("#UserName#", user3.name);
                th = th.replace("#UserID#", user3.id);
                th = th.replace("#UserID#", user3.id);
                th = th.replace("#UserID#", user3.id);
                grid = grid.replace("#Col3#", th);
            }
            else
            {
                grid = grid.replace("#Col3#", '');
            }

            result += grid;

            return result;
        };

        var UsersForm = function()
        {
            var form = '';

            if (API().Users != undefined && API().Users != null)
            {
                for(var i = 0; i < API().Users.length; i=i+3)
                {
                    form = form +
                    GetThumbnail(
                        API().Users.length > (i) ? API().Users[i] : null,
                        API().Users.length > (i+1) ? API().Users[i+1] : null,
                        API().Users.length > (i+2) ? API().Users[i+2] : null);
                }
            }

            return form;
        };

        var GetUserByID = function(id)
        {
            for(var i = 0; i < window.Users.length; i++)
            {
                if (window.Users[i].id == id)
                    return window.Users[i];
            }

            return null;
        };

        return {
            UsersForm: UsersForm,

            GetUserByID: GetUserByID
        }
    };

    var OnPhotoClick = function(id)
    {
        if (window.IdSender == undefined || window.IdSender == null)
        {
            window.IdSender = id;
            $('#thumbnail-'+id).css('border-color', "#0460be");
            return null;
        }

        if (window.IdRecipient == undefined || window.IdRecipient == null)
        {
            window.IdRecipient = id;
            $('#thumbnail-'+id).css('border-color', "#bd370d");
            return null;
        }

        return null;
    };

    var ItemsList = function()
    {
        var ROW_SCHEMA =
            '<tr>'+
                '<th scope="row">#row-id#</th>'+
                '<td>#row-recipient#</td>'+
                '<td>#row-sender#</td>'+
                '<td>#row-item#</td>'+
                '<td><a href="#row-xml#">Скачать xml</a></td>'+
            '</tr>';

        var GetItemRow = function(rowId, recipient, sender, item, xml)
        {
            var th = ROW_SCHEMA;
            th = th.replace("#row-id#", rowId);
            th = th.replace("#row-recipient#", recipient);
            th = th.replace("#row-sender#", sender);
            th = th.replace("#row-item#", item);
            th = th.replace("#row-xml#", xml);

            return th;
        };

        var GetItemsList = function()
        {
            var rows = '';
            if (window.Invoices != undefined && window.Invoices != null)
            {
                for(var i = 0; i < window.Invoices.length; i++)
                {
                    var rowId = i;
                    var recipient = Content().UsersList().GetUserByID(window.Invoices[i].id_recipient).name;
                    var sender = Content().UsersList().GetUserByID(window.Invoices[i].id_sender).name;
                    var itemName = window.Invoices[i].item_name;
                    var xmlURI = window.Invoices[i].xml_uri;

                    var row = GetItemRow(rowId, recipient, sender, itemName, xmlURI);
                    rows = rows + row;
                }
            }

            return rows;
        };

        return {
            GetItemsList: GetItemsList
        };
    };

    return {
        UsersList: UsersList,
        ItemsList: ItemsList,

        OnPhotoClick: OnPhotoClick
    };
};
