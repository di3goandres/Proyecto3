// JavaScript Document
$(function(){
  
  //Desplegar menus automaticamente
  $('ul.nav li.dropdown').hover(
      function(){
          //función despliegue
          $('.dropdown-menu', this).fadeIn();
      },
      function(){
          //función de contracción
          $('.dropdown-menu',this ).fadeOut('fast');
      }
    );
});

