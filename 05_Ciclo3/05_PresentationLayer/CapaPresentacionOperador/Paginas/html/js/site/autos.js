$(document).ready(function(){
 (function( $ ) {
    $.widget( "custom.combobox", {
      _create: function() {
        this.wrapper = $( "<span class='arrow-caret'>" )
          .addClass( "custom-combobox" )
          .insertAfter( this.element );
 
        this.element.hide();
        this._createAutocomplete();
        this._createShowAllButton();
      },
 
      _createAutocomplete: function() {
        var selected = this.element.children( ":selected" ),
          value = selected.val() ? selected.text() : "";
 
        this.input = $( "<input>" )
          .appendTo( this.wrapper )
          .val( value )
          .attr( "title", "" )
          .addClass( "custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left" )
          .autocomplete({
            delay: 0,
            minLength: 0,
            source: $.proxy( this, "_source" )
          })
         
 
        this._on( this.input, {
          autocompleteselect: function( event, ui ) {
            ui.item.option.selected = true;
            this._trigger( "select", event, {
              item: ui.item.option
            });
          },
 
          autocompletechange: "_removeIfInvalid"
        });
      },
 
      _createShowAllButton: function() {
        var input = this.input,
          wasOpen = false;
 
        $( "<a>" )
          .attr( "tabIndex", -1 )
          .attr( "title", "" )
          .appendTo( this.wrapper )
          .button({
            icons: {
              primary: "ui-icon-triangle-1-s"
            },
            text: false
          })
          .removeClass( "ui-corner-all" )
          .addClass( "custom-combobox-toggle ui-corner-right" )
          .mousedown(function() {
            wasOpen = input.autocomplete( "widget" ).is( ":visible" );
          })
          .click(function() {
            input.focus();
 
            // Close if already visible
            if ( wasOpen ) {
              return;
            }
 
            // Pass empty string as value to search for, displaying all results
            input.autocomplete( "search", "" );
          });
      },
 
      _source: function( request, response ) {
        var matcher = new RegExp( $.ui.autocomplete.escapeRegex(request.term), "i" );
        response( this.element.children( "option" ).map(function() {
          var text = $( this ).text();
          if ( this.value && ( !request.term || matcher.test(text) ) )
            return {
              label: text,
              value: text,
              option: this
            };
        }) );
      },
 
      _removeIfInvalid: function( event, ui ) {
 
        // Selected an item, nothing to do
        if ( ui.item ) {
          return;
        }
 
        // Search for a match (case-insensitive)
        var value = this.input.val(),
          valueLowerCase = value.toLowerCase(),
          valid = false;
        this.element.children( "option" ).each(function() {
          if ( $( this ).text().toLowerCase() === valueLowerCase ) {
            this.selected = valid = true;
            return false;
          }
        });
 
        // Found a match, nothing to do
        if ( valid ) {
          return;
        }
 
       
      },
 
      _destroy: function() {
        this.wrapper.remove();
        this.element.show();
      }
    });
  })( jQuery );
 
  $(function() {
    $( ".combobox" ).combobox();
    $( "#toggle" ).click(function() {
      $( ".combobox" ).toggle();
    });
  });

InitAccesories();
InitPlanes();

function InitAccesories(){
		$(".accesories-check").on("click",function(){
			$(".accesorios").toggleClass("active");
			$(".accesories-check").removeClass('disabled');
			$(this).toggleClass('disabled');
			var n=$('.totalAccesory')
			$("html, body").animate({scrollTop:$(n).offset().top},1500);
		});}

function InitPlanes(){
		$(".stepPlan").on("click",function(){
			$(".quoteContent").addClass("destroy");
			$(".quotePlanes").addClass("show");
			$(".quotelinks li:first-child").removeClass("hidden-Datos").addClass('active');
			$(".myData").addClass("active").addClass('isMobile');
			var n=$('.stepQuote');
			$("html, body").animate({scrollTop:$(n).offset().top},800);
			$(".notification-mobile").addClass("ns-show").addClass("ns-hide");
		});

		$(".closeMyData").on("click",function(){
			$(".myData").removeClass("active");
			$(".quotelinks li:first-child").removeClass('active');
			$(".data-Box .iconFooter").removeClass("pulse");
		});
		$(".data-Box").on("click",function(){
			if ($(".myData.active")){
				$(".myData").removeClass("active");
			}
			$(".myData").toggleClass("active").removeClass('isMobile');
			$(".data-Box .iconFooter").removeClass("pulse");
			$(".quotelinks li:first-child").toggleClass('active');
			$(".notification-mobile").removeClass('ns-show').removeClass('ns-hide');
		});
		$(".quotePlanes .packAxa").on("click", function(){
			if(".packAxa.active"){
				$(".packAxa").removeClass("active");
			}
			$(this).toggleClass("active");
			var n=$('.packAxa');
			$("html, body").animate({scrollTop:$(n).offset().top},800);
		});
		}
		});