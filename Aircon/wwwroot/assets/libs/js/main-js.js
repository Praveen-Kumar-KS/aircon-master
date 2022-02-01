
jQuery(document).ready(function($) {
    'use strict';

    // ============================================================== 
    // Notification list
    // ============================================================== 
    if ($(".notification-list").length) {

        $('.notification-list').slimScroll({
            height: '250px'
        });

    }

    // ============================================================== 
    // Menu Slim Scroll List
    // ============================================================== 


    if ($(".menu-list").length) {
        $('.menu-list').slimScroll({

        });
    }

    // ============================================================== 
    // Sidebar scrollnavigation 
    // ============================================================== 

    if ($(".sidebar-nav-fixed a").length) {
        $('.sidebar-nav-fixed a')
            // Remove links that don't actually link to anything

            .click(function(event) {
                // On-page links
                if (
                    location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') &&
                    location.hostname == this.hostname
                ) {
                    // Figure out element to scroll to
                    var target = $(this.hash);
                    target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                    // Does a scroll target exist?
                    if (target.length) {
                        // Only prevent default if animation is actually gonna happen
                        event.preventDefault();
                        $('html, body').animate({
                            scrollTop: target.offset().top - 90
                        }, 1000, function() {
                            // Callback after animation
                            // Must change focus!
                            var $target = $(target);
                            $target.focus();
                            if ($target.is(":focus")) { // Checking if the target was focused
                                return false;
                            } else {
                                $target.attr('tabindex', '-1'); // Adding tabindex for elements not focusable
                                $target.focus(); // Set focus again
                            };
                        });
                    }
                };
                $('.sidebar-nav-fixed a').each(function() {
                    $(this).removeClass('active');
                })
                $(this).addClass('active');
            });

    }

    // ============================================================== 
    // tooltip
    // ============================================================== 
    if ($('[data-toggle="tooltip"]').length) {
            
            $('[data-toggle="tooltip"]').tooltip()

        }

     // ============================================================== 
    // popover
    // ============================================================== 
       if ($('[data-toggle="popover"]').length) {
            $('[data-toggle="popover"]').popover()

    }
     // ============================================================== 
    // Chat List Slim Scroll
    // ============================================================== 
        

        if ($('.chat-list').length) {
            $('.chat-list').slimScroll({
            color: 'false',
            width: '100%'


        });
    }
}); // AND OF JQUERY


$(document).ready(function () {
    $(".theme-loader").animate({
        opacity: "0"
    }, 1000);
    setTimeout(function () {
        $(".theme-loader").remove();
    }, 800);
});



$(document).ready(function () {
    // Add minus icon for collapse element which is open by default
    $(".collapse.show").each(function () {
        $(this).parent(".collapse-container").find(".collapse-icon mdi").addClass("mdi-fullscreen-exit").removeClass("mdi-fullscreen");
    });
    // Toggle plus minus icon on show hide of collapse element
    $(".collapse").on('show.bs.collapse', function () {
        $(this).parent(".collapse-container").find(".collapse-icon mdi").removeClass("mdi-fullscreen").addClass("mdi-fullscreen-exit");
        $(this).parent(".collapse-container").find(".collapse-text").html("COLLAPSE");
        WrapAndSaveBlockData($(this), false)
    }).on('hide.bs.collapse', function () {
        $(this).parent(".collapse-container").find(".collapse-icon mdi").removeClass("mdi-fullscreen-exit").addClass("mdi-fullscreen");
        $(this).parent(".collapse-container").find(".collapse-text").html("EXPAND");
        WrapAndSaveBlockData($(this), true)
    });

    $('.collapsefilterSelecter').click(function () {
        var action = $(this).data("action");
        var container = $(this).data("container");
        $("#" + container).load(action);
    });



     $(".showrecordcount").on('change paste', function () {
        var element = $(this);
        var dataAttribute = $(element).attr("data-recordcountattribute");
        var value = $(element).val();
         saveUserPreferences(rootAppPath + 'Customer/Preference/SaveIntPreference', dataAttribute, value);
         window.location.href = window.location.href;   
     });

    $(window).scroll(function () {
        sessionStorage.scrollTop = $(this).scrollTop();
    });
    $(document).ready(function () {
        if (sessionStorage.scrollTop != "undefined") {
            $(window).scrollTop(sessionStorage.scrollTop);
        }
    });
    //$('#EditCustomerUserProfile').on('show.bs.modal',
    //    function (e) {
    //        var Id = $(e.relatedTarget).attr('data-id');
    //        $('#EditCustomerUserProfileContent').load('/Customer/User/EditUsersPartial?id=' + Id);
    //    });
});


function WrapAndSaveBlockData(card, collapsed) {
    var hideAttribute = card.attr("data-hideAttribute");
    saveUserPreferences(rootAppPath + 'Customer/Preference/SavePreference', hideAttribute, collapsed);
}

function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};

function saveUserPreferences(url, name, value) {
    var postData = {
        name: name,
        value: value
    };
    addAntiForgeryToken(postData);
    $.ajax({
        cache: false,
        url: url,
        type: "POST",
        data: postData,
        dataType: "json",
        error: function (jqXHR, textStatus, errorThrown) {
            alert('Failed to save preferences.');
        },
        complete: function (jqXHR, textStatus) {
            $("#ajaxBusy span").removeClass("no-ajax-loader");
        }
    });

};
