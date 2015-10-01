$(document).ready(function(){
InitMenu();
InitHeader();
InitSearch();
SwitchHeader();
SwitchFooter();
ScrollToTop();

function ScrollToTop(){
	$(window).scroll(function(){
		var n=$(window).scrollTop();
		n>=200?$(".scrollto").addClass("icon-circle-up").fadeIn(500):$(".scrollto").removeClass("icon-circle-up").fadeOut()});
	$(".scrollto").click(function(){
		$("html, body").animate({scrollTop:0},600)}
		)}

function SwitchHeader(){
	$(window).on("scroll", function () {
    if ($(this).scrollTop() > 640) {
        $(".header.topFix").addClass("fixed");
    }
    else {
        $(".header.topFix").removeClass("fixed");
    }
});
}

function SwitchFooter(){
	$(window).on("scroll", function () {
    if ($(this).scrollTop() > 727) {
        $(".asesorOnline").addClass("relative");
    }
    else {
        $(".asesorOnline").removeClass("relative");
    }
});
}

function InitSearch(){
		$(".searchBox").on("click",function(){
			$(".search-form").toggleClass("active");
			$(".search-form .form-inline .form-control").toggleClass("inactive");
			$("#nav li:last-child").toggleClass("active");
		});
	}

function InitMenu(){
		$(".tools-nav li a").on("click",function(){
			$(".tools-nav li a").removeClass("active");
			var n=$(this).attr("href");
			$("html, body").animate({scrollTop:$(n).offset().top},500);
			$(this).parent().addClass("active");
			$(this).addClass("active");
		});
	}
function InitHeader(){
	$(".nav-btn").on("click", function(){
		$("html").addClass("js-nav");
	});
}

$('#myTabs a').click(function (e) {
  e.preventDefault()
  $(this).tab('show')
})

	$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
  e.target // newly activated tab
  e.relatedTarget // previous active tab
})
});