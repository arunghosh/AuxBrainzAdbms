$(function () {
    if ($('#tweetLstCtnr').length) {
        var $tweekFrm = $('#hmeTweekFrm');
        refreshTweeks();

        $tweekFrm.find('textarea').keyup(function (event) {
            $('#twCnt').html((1000 - $(this).val().length));
        });

        $tweekFrm.submit(function () {
            return false;
        });

        $tweekFrm.find('a').click(function () {
            var $msg = $tweekFrm.find('textarea');
            if ($msg.val().length > 0 && $msg.val().length < 1000) {
                $tweekFrm.find('.busy-img').show();
                $.ajax({
                    url: "/Admin/AlumniKnow/Edit",
                    type: "POST",
                    data: $tweekFrm.serialize(),
                    success: function () {
                        refreshTweeks();
                        $msg.val('').focus();
                        $tweekFrm.find('.busy-img').hide();
                    }
                });
            }
        });

        $('#tweekLst').slimScroll({
            height: 360
        });
    }
});

function refreshTweeks() {
    var $tweek = $('#tweekLst');
    $.ajax({
        url: 'Admin/AlumniKnow/Tweeks',
        type: 'Get',
        success: function (result) {
            $tweek.empty();
            for (var i = 0; i < result.length; i++) {
                var artc = result[i];
                var id = artc.UserId;

                var $like = $('<a>').addClass('tw-l-cnt').append($('<i>').addClass('icon-th-up'))
                                .append($('<span>').html(artc.lCnt))
                                .click(function () { updateTwAff($(this), 1); });
                var $dlike = $('<a>').addClass('tw-d-cnt').append($('<i>').addClass('icon-th-down'))
                            .append($('<span>').html(artc.dCnt))
                            .click(function () { updateTwAff($(this), 0); });


                var $left = $('<div>').addClass('tw-left').attr('data-id', artc.Id)
                                .append($('<img>').attr('src', '/Home/SmallImage/' + id))
                                .append($like)
                                .append($dlike);
                
                var re = new RegExp('-', 'g');
                artc.Time = artc.Time.replace(re, '/');
                var $time = $('<span>').addClass('time')
                            .html(getShortTime(artc.Time));

                var $info = $('<div>').addClass('tw-info').append($time)
                                .append($('<a>').attr('href', '/user/' + id).html(artc.UserName))
                                .append($('<span>').html(', ' + artc.Batch + ' batch'))
                                .append($('<div>').addClass('tw-txt').html(artc.Tweek));
                var $div = $('<div>').append($left).append($info);
                $tweek.append($('<li>').append($div));
            }
            if (result.length === 0) {
                $tweek.append($('<li>').html("No Tweets!!!"));
            }
        }
    });
}

function updateTwAff($this, status) {
    debugger;
    var $par = $this.parents('.tw-left');
    var id = $par.attr('data-id');
    $.ajax({
        url: 'Admin/AlumniKnow/UpdateAffinity',
        type: 'POST',
        data: { id: id, status: status },
        success: function (result) {
            $par.find('.tw-l-cnt span').html(result.l);
            $par.find('.tw-d-cnt span').html(result.d);
        }
    });
}

$(function () {
    var $head = $('#menu');
    if ($head.length > 0) {
        var previousScroll = 0,
        headerOrgOffset = $head.offset().top;
        $(window).scroll(function () {
            var currentScroll = $(this).scrollTop();
            if (currentScroll > previousScroll) {
                $head.removeClass('fixed');
                $head.fadeOut();
            } else {
                $head.fadeIn();
                $head.addClass('fixed');
            }
            previousScroll = currentScroll;
        });
    }

    $('.cont-req-frm a').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            $this.parent('form').submit();
        });
    });

    $('.cont-req-frm form').submit(function () {
        $this = $(this);
        showBusy($this);
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                $this.parents('.cont-req-frm')
                    .html("Request Send");
            }
        });
        return false;
    });

    $('.lazy-load').each(function () {
        refreshLazy($(this));
    });

    // New Pop-up for Message/Discssion/Job Post
    $('.new-obj').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            showDialog($this, null);
        })
    });

    $('.note-close').click(function () {
        $(this).parents('.status-note').hide();
    });

    // Good
    $('.menu-drop').mouseenter(function () {
        $(this).find('.drop-ctnt').show();
        $(this).addClass('act-menu');
    });

    // Good
    $('.menu-drop').mouseleave(function () {
        $(this).find('.drop-ctnt').hide();
        $(this).removeClass('act-menu');
    });

    var $nmeSrch = $("#nameSearchCtnr");
    if ($nmeSrch.length === 1) {
        $nmeSrch.find('img').click(function () {
            $nmeSrch.find('form').submit();
        });
        $nmeSrch.find('input').keyup(function (event) {
            if (event.keyCode == 13) {
                $nmeSrch.find('form').submit();
            }
        });
        $nmeSrch.find('input').autocomplete({
            select: function (a, b) {
                $(this).val(b.item.value);
                $nmeSrch.find('form').submit()
            }
        });
    }

    $(document).ajaxError(function (xhr, props) {
        if (props.status === 401) {
            location.reload();
        }
    });
});

function refreshLazy($this) {
    if ($this.attr('data-loaded') === undefined) {
        var url = $this.attr('data-url');
        if ($this.attr('data-busy') !== 'false') {
            $this.append('<img src="/Content/images/busy.gif"/>')
        }
        $.ajax({
            url: url,
            type: 'Get',
            data: { id: $this.attr('data-id') },
            success: function (result) {
                $this.attr('data-loaded', true);
                $this.html(result);
            }
        });
    }
}

// Display Dialog
function showDialog($this, data) {
    var $ctnr = $("#dialogCtnr");
    showAppBusy($this.attr('data-title'));
    $ctnr.dialog({
        modal: true, autoOpen: false, resizable: false,
        width: 'auto', title: $this.attr('data-title'), draggable: false
    });
    var url = $this.attr('data-url');
    $.ajax({
        url: url,
        type: $this.attr('data-method') === undefined ? "POST" : "GET",
        traditional: true,
        data: { userIds: data, offset: new Date().getTimezoneOffset() },
        success: function (result) {
            hideAppBusy();
            $ctnr.html(result);
            $ctnr.dialog("open");
        }
    });
}

function refreshForm(ctnr) {
    showBusy(ctnr);
    var id = ctnr.attr('data-id');
    var url = ctnr.attr('data-url');
    $.ajax({
        url: url,
        type: 'Get',
        data: { id: id },
        success: function (result) {
            ctnr.html(result);
            clearBusy(ctnr);
        }
    });
    try {
        updateUrl(id);
    } catch (err) { }

}

function updateUrl(id) {
    var loc = document.URL,
    index = loc.indexOf('#');
    var currUrl = (index > 0) ? loc.substring(0, index) : loc;
    var urlArr = currUrl.split('/');
    var intRegex = /^\d+$/;
    if (intRegex.test((urlArr[urlArr.length - 1]))) {
        window.history.pushState("", "", "/" + urlArr[urlArr.length - 2] + "/" + id);
    }
    else {
        window.history.pushState("", "", "/" + urlArr[urlArr.length - 1] + "/" + id);
    }
}

$(function () {
    $("input[data-autocomplete-source]").each(function () {
        var target = $(this);
        target.autocomplete({ source: target.attr("data-autocomplete-source") });
    });

    $('body').unbind('click');
    $('body').click(function (event) {
        // Get the focused element:
        var noEle = $(event.target).parents('.ntfy-lst').length + $(event.target).parents('.popover').length + $(event.target).parents('.notify-icon').length;
        if (noEle === 0) {
            $('.popover').hide();
            $('.ntfy-ctnr').hide();
        }

    });

    $("#busyIndicator").hide();
    axbOnResize();
});

function callAjax(form, callBack) {
    $.ajax({
        url: form.action,
        type: form.method,
        data: $(form).serialize(),
        success: function (result) {
            if (callBack !== undefined) {
                callBack(result);
            }
        }
    });
}

function showFullBusy() {
    $('#overBk').show();
    $('#overCnt').show();
}

function showFullBusy() {
    $('#overBk').hide();
    $('#overCnt').hide();
}

function showAppBusy(title) {
    $("#busyIndicator").dialog({
        modal: true, autoOpen: false, resizable: false,
        width: 'auto', title: title, draggable: false
    });
    $("#busyIndicator").dialog('open');
}

function hideAppBusy() {
    $("#busyIndicator").dialog('close');
}



function axbOnResize() {
    var avaiHeight = $(window).height() - $('#header').height() - 55;

    $('#mainContent').css('min-height', avaiHeight + 70);

    $('.msg-thread').slimScroll({
        height: avaiHeight - $('#threadNewMsg').height()
    });

    //$('.msg-thread').height(avaiHeight);

    $('.avail-ht').slimScroll({
        height: avaiHeight
    });

    $('.pop-up-ht').slimScroll({
        height: avaiHeight - 55
    });
}


function showFrmBusy($this) {
    $this.find('.frm-busy').show();
}

function hideFrmBusy($this) {
    $this.find('.frm-busy').hide();
}
function showBusy($this) {
    $this.find('.frm-busy').show();
    $this.css("opacity", "0.7");
}

function clearBusy($this) {
    $this.find('.frm-busy').hide();
    $this.css("opacity", "1");
}

$(".a-sele-item").each(function () {
    var $this = $(this);
    $this.unbind('click');
    $this.click(function () {
        var ctnr = $(".a-frm-ctnr");
        ctnr.html('<img src="/Content/images/busy.gif"/>');
        $(".a-sele-item").removeClass('seleted');
        $this.addClass('seleted');
        var id = $(this).attr('data-id')
        ctnr.attr('data-id', id);
        refreshForm(ctnr);
    });
});

$(function () {
    $('.a-msg-new').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            var $div = $this.next('div');
            var id = $this.attr('data-id');
            $.ajax({
                url: '/Message/NewMessagePop',
                type: 'Get',
                data: { userId: id },
                success: function (result) {
                    $div.html(result);
                    $div.find('.popover').show();
                    clearBusy($this);
                }
            });
        });
    });

    $('.a-add-circle').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            var $div = $this.next('div');
            $div.html('<div class="arrow"></div><img src="/Content/images/busy.gif" class="p20" />');
            var id = $(this).attr('data-id');
            $.ajax({
                url: '/Circle/AddUser',
                type: 'Get',
                data: { userId: id },
                success: function (result) {
                    $div.html(result);
                    $div.find('.popover').show();
                    clearBusy($this);
                }
            });
        });
    });
});