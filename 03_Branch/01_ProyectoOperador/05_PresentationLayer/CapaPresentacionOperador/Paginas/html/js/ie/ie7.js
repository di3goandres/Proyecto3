/* To avoid CSS expressions while still supporting IE 7 and IE 6, use this script */
/* The script tag referencing this file must be placed before the ending body tag. */

/* Use conditional comments in order to target IE 7 and older:
	<!--[if lt IE 8]><!-->
	<script src="ie7/ie7.js"></script>
	<!--<![endif]-->
*/

(function() {
	function addIcon(el, entity) {
		var html = el.innerHTML;
		el.innerHTML = '<span style="font-family: \'axa\'">' + entity + '</span>' + html;
	}
	var icons = {
		'icon-axa_01': '&#xe600;',
		'icon-axa_02': '&#xe601;',
		'icon-axa_03': '&#xe602;',
		'icon-axa_04': '&#xe603;',
		'icon-axa_05': '&#xe604;',
		'icon-axa_06': '&#xe605;',
		'icon-axa_07': '&#xe606;',
		'icon-axa_08': '&#xe607;',
		'icon-axa_09': '&#xe608;',
		'icon-axa_10': '&#xe609;',
		'icon-axa_11': '&#xe60a;',
		'icon-axa_12': '&#xe60b;',
		'icon-axa_13': '&#xe60c;',
		'icon-axa_14': '&#xe60d;',
		'icon-axa_15': '&#xe60e;',
		'icon-axa_16': '&#xe60f;',
		'icon-axa_17': '&#xe610;',
		'icon-axa_18': '&#xe611;',
		'icon-axa_19': '&#xe612;',
		'icon-axa_20': '&#xe613;',
		'icon-axa_21': '&#xe614;',
		'icon-axa_22': '&#xe615;',
		'icon-axa_23': '&#xe616;',
		'icon-axa_24': '&#xe617;',
		'icon-axa_25': '&#xe618;',
		'icon-axa_26': '&#xe619;',
		'icon-axa_27': '&#xe61a;',
		'icon-axa_28': '&#xe61b;',
		'icon-axa_29': '&#xe61c;',
		'icon-axa_30': '&#xe61d;',
		'icon-axa_31': '&#xe61e;',
		'icon-axa_32': '&#xe61f;',
		'icon-axa_33': '&#xe620;',
		'icon-axa_34': '&#xe621;',
		'icon-axa_35': '&#xe622;',
		'icon-axa_36': '&#xe623;',
		'icon-axa_37': '&#xe624;',
		'icon-axa_38': '&#xe625;',
		'icon-axa_39': '&#xe626;',
		'icon-axa_40': '&#xe627;',
		'icon-axa_41': '&#xe628;',
		'icon-axa_42': '&#xe629;',
		'icon-axa_43': '&#xe62a;',
		'icon-axa_44': '&#xe62b;',
		'icon-axa_45': '&#xe62c;',
		'icon-axa_46': '&#xe62d;',
		'icon-axa_47': '&#xe62e;',
		'icon-axa_48': '&#xe62f;',
		'icon-axa_49': '&#xe630;',
		'icon-axa_50': '&#xe631;',
		'icon-axa_51': '&#xe632;',
		'icon-axa_52': '&#xe633;',
		'icon-axa_54': '&#xe634;',
		'icon-axa_55': '&#xe635;',
		'icon-axa_56': '&#xe636;',
		'icon-axa_57': '&#xe637;',
		'icon-axa_58': '&#xe638;',
		'icon-axa_59': '&#xe639;',
		'icon-axa_60': '&#xe63a;',
		'icon-axa_61': '&#xe63b;',
		'icon-axa_62': '&#xe63c;',
		'icon-axa_63': '&#xe63d;',
		'icon-axa_64': '&#xe63e;',
		'icon-axa_65': '&#xe63f;',
		'icon-axa_66': '&#xe640;',
		'icon-axa_67': '&#xe641;',
		'icon-axa_68': '&#xe642;',
		'icon-axa_69': '&#xe643;',
		'icon-axa_70': '&#xe644;',
		'icon-axa_71': '&#xe645;',
		'icon-axa_72': '&#xe646;',
		'icon-axa_73': '&#xe647;',
		'icon-axa_74': '&#xe648;',
		'icon-axa_75': '&#xe649;',
		'icon-axa_76': '&#xe64a;',
		'icon-axa_77': '&#xe64b;',
		'icon-axa_78': '&#xe64c;',
		'icon-axa_79': '&#xe64d;',
		'icon-axa_80': '&#xe64e;',
		'0': 0
		},
		els = document.getElementsByTagName('*'),
		i, c, el;
	for (i = 0; ; i += 1) {
		el = els[i];
		if(!el) {
			break;
		}
		c = el.className;
		c = c.match(/icon-[^\s'"]+/);
		if (c && icons[c[0]]) {
			addIcon(el, icons[c[0]]);
		}
	}
}());
