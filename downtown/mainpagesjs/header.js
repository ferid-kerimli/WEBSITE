const menuIcon = document.getElementById('menu-icon-header');
const curtainMenus = document.getElementById('curtainMenu');
const main=document.querySelector('.Main');
const body=document.querySelector('.Body');
const menuIconI = document.querySelector('.menu-icon i');
const main_header=document.querySelector('.header');
const A=document.getElementById('a-header');
const curtainMenu=document.querySelectorAll('.curtain-menu');
function getElementColor(element) {
    return window.getComputedStyle(element).color; 
}
menuIcon.addEventListener('click', function() {
    if (menuIcon.classList.contains('active')) {
        curtainMenus.classList.remove('active'); 
        menuIcon.classList.remove('active');
        const currentColor=getElementColor(A);
        menuIcon.innerHTML = `
             <span class="menu-icon-1 menu" id="menu1" style="background-color: ${currentColor}"></span>
             <span class="menu-icon-2 menu" id="menu2" style="background-color: ${currentColor}"></span>
             <span class="menu-icon-3 menu" id="menu3" style="background-color: ${currentColor}"></span>
        `;
    } else {
        curtainMenu.forEach(menu=>{
            menu.classList.remove('slidedown');
            void menu.offsetWidth;
            menu.classList.add('slidedown');
        });
        menuIcon.classList.add('active');
        curtainMenus.classList.add('active');
        const currentColor=getElementColor(A);
        menuIcon.innerHTML = `<i class="fa-solid fa-x" style="color: ${currentColor}"></i>`; 
    }
});

const menuLinks = document.querySelectorAll('.curtain-menu');
const homepagelists=document.querySelectorAll('.homepagelists');
menuLinks.forEach(link => {
        link.addEventListener('click', function() {
            this.classList.toggle('active');
            const homepagelink=link.nextElementSibling;
            homepagelink.classList.toggle('active');
            homepagelists.forEach(list=>{
                if(list!==homepagelink){
                    list.classList.remove('active');
                }
            });
            menuLinks.forEach(othermenu=>{
                if(othermenu!==this){
                    othermenu.classList.remove('active');
                }
            });
        });
    });