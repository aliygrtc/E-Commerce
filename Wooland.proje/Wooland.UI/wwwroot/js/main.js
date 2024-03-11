(function($) {
	$(document).ready(function() {
		// TOGGLE MOBILE MENU
		$('.ofm li.m_nav').on('click', function() {
			$(".mobi-menu").slideToggle("slow");
			$(this).find(".mobi-menu").slideToggle("slow");
		}
		);
		$('#cssmenu li.has-sub>a').on('click', function() {
			$(this).removeAttr('href');
			var element = $(this).parent('li');
			if (element.hasClass('open')) {
				element.removeClass('open');
				element.find('li').removeClass('open');
				element.find('ul').slideUp();
			} else {
				element.addClass('open');
				element.children('ul').slideDown();
				element.siblings('li').children('ul').slideUp();
				element.siblings('li').removeClass('open');
				element.siblings('li').find('li').removeClass('open');
				element.siblings('li').find('ul').slideUp();
			}
		});
		$('#cssmenu>ul>li.has-sub>a').append('<span class="holder"></span>');
		(function getColor() {
			var r, g, b;
			var textColor = $('#cssmenu').css('color');
			textColor = textColor.slice(4);
			r = textColor.slice(0, textColor.indexOf(','));
			textColor = textColor.slice(textColor.indexOf(' ') + 1);
			g = textColor.slice(0, textColor.indexOf(','));
			textColor = textColor.slice(textColor.indexOf(' ') + 1);
			b = textColor.slice(0, textColor.indexOf(')'));
			var l = rgbToHsl(r, g, b);
			if (l > 0.7) {
				$('#cssmenu>ul>li>a').css('text-shadow', '0 1px 1px rgba(0, 0, 0, .35)');
				$('#cssmenu>ul>li>a>span').css('border-color', 'rgba(0, 0, 0, .35)');
			} else {
				$('#cssmenu>ul>li>a').css('text-shadow', '0 1px 0 rgba(255, 255, 255, .35)');
				$('#cssmenu>ul>li>a>span').css('border-color', 'rgba(255, 255, 255, .35)');
			}
		})();

		function rgbToHsl(r, g, b) {
			r /= 255, g /= 255, b /= 255;
			var max = Math.max(r, g, b),
				min = Math.min(r, g, b);
			var h, s, l = (max + min) / 2;
			if (max == min) {
				h = s = 0;
			} else {
				var d = max - min;
				s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
				switch (max) {
					case r:
						h = (g - b) / d + (g < b ? 6 : 0);
						break;
					case g:
						h = (b - r) / d + 2;
						break;
					case b:
						h = (r - g) / d + 4;
						break;
				}
				h /= 6;
			}
			return l;
		}
		
		
		// accordion
		
		$('.collapse').on('shown.bs.collapse', function(){
			$(this).parent().find(".fa-plus").removeClass("fa-plus").addClass("fa-minus");
			}).on('hidden.bs.collapse', function(){
			$(this).parent().find(".fa-minus").removeClass("fa-minus").addClass("fa-plus");
		});
	
	
		 // This button will increment the value
		$('.qtyplus').click(function(e){
			// Stop acting like a button
			e.preventDefault();
			// Get the field name
			fieldName = $(this).attr('field');
			// Get its current value
			var currentVal = parseInt($('input[name='+fieldName+']').val());
			// If is not undefined
			if (!isNaN(currentVal)) {
				// Increment
				$('input[name='+fieldName+']').val(currentVal + 1);
			} else {
				// Otherwise put a 0 there
				$('input[name='+fieldName+']').val(0);
			}
		});
		// This button will decrement the value till 0
		$(".qtyminus").click(function(e) {
			// Stop acting like a button
			e.preventDefault();
			// Get the field name
			fieldName = $(this).attr('field');
			// Get its current value
			var currentVal = parseInt($('input[name='+fieldName+']').val());
			// If it isn't undefined or its greater than 0
			if (!isNaN(currentVal) && currentVal > 0) {
				// Decrement one
				$('input[name='+fieldName+']').val(currentVal - 1);
			} else {
				// Otherwise put a 0 there
				$('input[name='+fieldName+']').val(0);
			}
		});
	
		 // This button will increment the value
		$('.qtyplus2').click(function(e){
			// Stop acting like a button
			e.preventDefault();
			// Get the field name
			fieldName = $(this).attr('field');
			// Get its current value
			var currentVal = parseInt($('input[name='+fieldName+']').val());
			// If is not undefined
			if (!isNaN(currentVal)) {
				// Increment
				$('input[name='+fieldName+']').val(currentVal + 1);
			} else {
				// Otherwise put a 0 there
				$('input[name='+fieldName+']').val(0);
			}
		});
		// This button will decrement the value till 0
		$(".qtyminus2").click(function(e) {
			// Stop acting like a button
			e.preventDefault();
			// Get the field name
			fieldName = $(this).attr('field');
			// Get its current value
			var currentVal = parseInt($('input[name='+fieldName+']').val());
			// If it isn't undefined or its greater than 0
			if (!isNaN(currentVal) && currentVal > 0) {
				// Decrement one
				$('input[name='+fieldName+']').val(currentVal - 1);
			} else {
				// Otherwise put a 0 there
				$('input[name='+fieldName+']').val(0);
			}
		});
	
		$('.qtyplus, .qtyminus').attr('field','quantity');
		$('.qtyplus2, .qtyminus2').attr('field','quantity');
	
		//Owl carousel
		$('.owl-carousel').owlCarousel({
				autoplay:100,
				loop:true,
				margin:0,
				nav:true,
				responsive:{
					0:{
						items:1
					},
					600:{
						items:2
					},
					1000:{
						items:4
					}
				}
			})
		
	
	});
	
	// Preloader js
	jQuery(window).load(function(){
		
		$('body, html').removeClass('fix');
		
		$(".preloader_wrapper").fadeOut(1000);
		
	});
	
	
	getCartCount()
})(jQuery);


function getCartCount() {
	$.ajax({
		type: "GET",
		url: "sepet/sepetsayisi",
		success: function (data) {
			if (data == 0) {
				$(".head_cart_text small").text("0")
			}
			else {
				$(".head_cart_text small").text(data)
			}
		}
	});
}

function addCart(productid, stock) {
	var istenenMiktar = $(".cart-plus-minus-box").val()
	if (istenenMiktar <= 0) {
		alert("1'den az ürün sipariş edilemez!")
		
	}
	else if (istenenMiktar <= stock) {
		$.ajax({
			type: "POST",
			url: "/sepet/sepeteekle",
			data: { "productid": productid, "quantity": istenenMiktar },
			success: function (data) {
				if (data.indexOf("~") == -1) {
					// $("#modalSepet .modal-body").text(istenenMiktar + " adet ürün sepetinize eklenmiştir...")
					alert(istenenMiktar + " adet ürün sepetinize eklenmiştir...")
					// $("#modalSepet").modal('show')
				} else {
					location.href = "/user/login"
				}
			},
			error: function (e) {
				alert(e.responseText)
			}
		});
	} else {
		alert("Bu üründen en fazla " + stock + " tane sipariş verebilirsiniz!")
		// $("#modalSepet").modal('show')
	}
}

function removeCart(productid) {
	$.ajax({
		type: "POST",
		url: "/sepet/sepettensil",
		data: { "productid": productid },
		success: function (data) {
			if (data == "OK") {
				location.href = "/sepet"
			}
		},
		error: function (e) {
			alert(e.responseText)
		}
	});
}


