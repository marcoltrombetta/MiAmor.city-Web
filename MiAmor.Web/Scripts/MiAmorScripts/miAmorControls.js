var dragging = false;
$.fn.uouCustomSelect = function () {
    var $select = $(this);

    // $select.wrap('<div class="uou-custom-select"></div>');

    var $container = $select.parent('.uou-custom-select');

    // $container.append('<ul class="select-clone"></ul>');

    var $list = $container.children('.select-clone'),
      placeholder = $select.data('placeholder') ? $select.data('placeholder') : $select.find('option:eq(0)').text();

    // $('<input class="value-holder" type="text" disabled="disabled" placeholder="' + placeholder + '"><i class="fa fa-chevron-down"></i>').insertBefore($list);
    // $('<input class="value-holder" type="hidden" disabled="disabled" placeholder="' + placeholder + '"><span class="placeholder">' + placeholder + '</span><i class="fa fa-chevron-down"></i>').insertBefore($list);

    var $valueHolder = $container.children('.value-holder');
    var $valuePlaceholder = $container.children('.placeholder');

    // Create clone list
    //$select.find('option').each(function () {
    //    var $this = $(this);

    //    $list.append('<li data-value="' + $this.val() + '">' + $this.text() + '</li>');
    //});

    // Toggle list
    $container.on('click', function () {
        // console.log('click ' + $container);
        $container.toggleClass('active');
        $list.slideToggle(250);
    });

    // Option Select
    $list.children('li').on('click', function () {
        var $this = $(this);

        $valueHolder.val($this.attr("data-value"));
        $valuePlaceholder.html($this.text());
        $select.find('option[value="' + $this.data('value') + '"]').prop('selected', true);
    });

    // Hide
    $container.on('clickoutside touchendoutside', function () {
        if (!dragging) {
            $container.removeClass('active');
            $list.slideUp(250);
        }
    });

    // Links
    if ($select.hasClass('links')) {
        $select.on('change', function () {
            window.location.href = select.val();
        });
    }

    $select.on('change', function () {
        cosole.log(chnaged);
        cosole.log($(this).val());
    });
};
function setSelectuou(drag) {
    dragging = drag;
    $('select').each(function () {
        $(this).uouCustomSelect();
    });
};
