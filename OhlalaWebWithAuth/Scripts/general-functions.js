function iterateAllEventImages() {
    /* Check the location of each desired element */
    $('.eventImages').each(function (i) {

        var height_of_object = $(this).outerHeight();
        var top_of_object = $(this).position().top;
        var bottom_of_window = $(window).scrollTop() + $(window).height();

        var position_to_display = top_of_object + height_of_object / 3;

        /* If the object is completely visible in the window, fade it */
        if (bottom_of_window > position_to_display) {
            $(this).animate({ 'opacity': '1' }, 500);
        }
    });
}

function iterateAllEntryEventSections() {
    /* Check the location of each desired element */
    $('.entryEventSection').each(function (i) {

        var height_of_object = $(this).outerHeight();
        var top_of_object = $(this).position().top;
        var bottom_of_window = $(window).scrollTop() + $(window).height();

        var position_to_display = top_of_object + height_of_object / 3;

        /* If the object is completely visible in the window, fade it */
        if (bottom_of_window > position_to_display) {
            $(this).animate({ 'opacity': '1' }, 500);
        }
    });
}