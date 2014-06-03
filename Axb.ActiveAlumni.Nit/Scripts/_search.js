$(function () {
    $("#pageCtnr li").each(function () {
        $(this).click(function () {
            submitSearch($(this).html());
        });
    });

    $("#pageMiniCtnr li").each(function () {
        $(this).click(function () {
            submitSearch($(this).attr('data-page'));
        });
    });
});

$(function () {
    $('.exp-title').each(function () {
        $(this).click(function () {
            toggleFilter(this);
        });
    });

    $("[data-exp-c]").each(function () {
        toggleFilter(this);
    });

});

function toggleFilter(expTitle) {
    var id = $(expTitle).attr('data-exp-target');
    var $target = $('#' + id);
    $expTitle = $(expTitle);
    $expIcon = $expTitle.find('.exp-icon');
    if ($target.css('display') == 'none') {
        $expIcon.removeClass('icon-ch-r');
        $expIcon.addClass('icon-ch-d');
        $expTitle.find('input').val('true');
    }
    else {
        $expIcon.removeClass('icon-ch-d');
        $expIcon.addClass('icon-ch-r');
        $expTitle.find('input').val('false');
    }
    $target.toggle('slide', { direction: 'up' });
}

$(function () {
    $("#filterCtnr input").each(function () {
        $(this).removeAttr('disabled');
        $(this).change(function () {
            var $this = $(this);
            if ($this.val() === '_all_') {
                var grpName = $this.attr('data-group');
                $('input[name=' + grpName + ']').removeAttr('checked');
                $this.prop('checked', true);
            } else {
                var grpName = $this.attr('name');
                $('#' + grpName).prop('checked', !$("[name=" + grpName + "]").is(':checked'));
            }
            submitSearch();
        });
    });

    $("#filterCtnr input[type=text]").keydown(function (event) {
        if (event.keyCode === 9) {
            $(this).val("");
        }
    });

    $("#filterCtnr input[type=text]").autocomplete({
        select: function (a, b) {
            $(this).val(b.item.value);
            submitSearch();
        }
    });
});

function submitSearch(pageNo) {
    $('#overBk').show();
    $('#overCnt').show();
    pageNo = pageNo === undefined ? 1 : pageNo;
    $("#PageIndex").val(pageNo);
    $("#filterCtnr form").submit();
}
