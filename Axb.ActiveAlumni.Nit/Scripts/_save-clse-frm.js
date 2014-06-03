$(function () {

    $('.save-clse-frm').find('.validation-summary-errors').hide();
    $('.save-clse-frm').find('.frm-busy').hide();
    $('.btn-cancel').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            closePopup($this);
        });
    });

    $('.btn-hide').each(function () {
        var $this = $(this);
        var $form = $this.closest('form');
        $this.unbind('click');
        $this.click(function () {
            $form.find('#IsDeleted').val('True');
            $form.submit();
        });
    });

    $('.btn-unhide').each(function () {
        var $this = $(this);
        var $form = $this.closest('form');
        $this.unbind('click');
        $this.click(function () {
            $form.find('#IsDeleted').val('False');
            $form.submit();
        });
    });

    $('.save-clse-frm').each(function () {
        var $this = $(this);
        $this.unbind('submit');
        $this.submit(function () {
            showFrmBusy($this);
            $this.find(".dtpicker").each(function () {
                var $date = $(this);
                var utc = new Date($date.val());
                utc.setMinutes(utc.getMinutes() + utc.getTimezoneOffset());
                $date.val(utc.toLocaleString());
            });
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    if (result.errMsg === undefined || result.errMsg === null) {
                        debugger;
                        if (result.url !== undefined) {
                            location.assign(result.url);
                        }
                        else if ($this.attr('data-refresh') === 'true') {
                            location.reload();
                        }
                        else {
                            closePopup($this);
                        }
                    }
                    else {
                        var errMsg = result.errMsg;
                        var $val = $this.find('.validation-summary-errors');
                        $val.show().text(errMsg);
                    }
                    hideFrmBusy($this);
                }
            });
            return false;
        });
    });

    $('.ntfy-ctnr .close-btn').click(function () {
        var $this = $(this);
        $this.parents('.ntfy-ctnr').hide();
    });
});


function closePopup($this) {
    $this.parents('.ntfy-ctnr').hide();
    $this.parents('.popover').hide();
    $this.closest('.ui-dialog-content').dialog('close');
}