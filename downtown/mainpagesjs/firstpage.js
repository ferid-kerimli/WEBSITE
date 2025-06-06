const images = ['/downtown/images/mainimages/Saytqlavniybanner.jpg', '/downtown/images/mainimages/Saytqlavniybanner360.jpg', '/downtown/images/mainimages/Saytqlavniybannerphoto.jpg'];
let currentIndex = 0;
const section = document.getElementById('section1');

function updateBackground() {
  section.style.setProperty('--bg-image', `url(${images[currentIndex]})`);
  }

function nextImage() {
    section.style.setProperty('--scale-y', '0');
  setTimeout(() => {
    currentIndex = (currentIndex + 1) % images.length;
    updateBackground();
    section.style.setProperty('--scale-y', '1');
}, 500);
}

function previousImage() {
    section.style.setProperty('--scale-y','0'); 
  setTimeout(() => {
    currentIndex = (currentIndex - 1 + images.length) % images.length;
    updateBackground();
    section.style.setProperty('--scale-y', '1');
  }, 500);
}

const circle2=document.querySelector('.circle2');
const circle1=document.querySelector('.circle1');
const fa_arrow_down_long=document.querySelector('.fa-arrow-down-long');
circle1.addEventListener('mouseover', ()=>{
    circle2.style.transform='scale(1.2)';
});
circle2.addEventListener('mouseover', ()=>{
    circle2.style.transform='scale(1.2)';
});
circle1.addEventListener('mouseout', ()=>{
    circle2.style.transform='scale(1)';
});
circle2.addEventListener('mouseout', ()=>{
    circle2.style.transform='scale(1)';
});
fa_arrow_down_long.addEventListener('mouseover', ()=>{
    circle2.style.transform='scale(1.2)';
});

fa_arrow_down_long.addEventListener('mouseout', ()=>{
    circle2.style.transform='scale(1)';
});

const arrows=document.querySelectorAll('.arrow');
arrows.forEach(arrow=>{
    arrow.addEventListener('mouseover', ()=>{
    arrow.style.transform='scale(1.1)';
});
    arrow.addEventListener('mouseout', ()=>{
    arrow.style.transform='scale(1)';
});
});

const whatwedo=document.querySelector('.whatwedo');
const i1=document.querySelector('.i1');

whatwedo.addEventListener('mouseover',()=>{
    whatwedo.style.transform='scale(1.02)';
    i1.style.transform='scale(1.1)';
});
whatwedo.addEventListener('mouseout', ()=>{
    whatwedo.style.transform='scale(1)';
    i1.style.transform='scale(1)';
});

const viewworks=document.querySelector('.viewworks');
const i2=document.querySelector('.i2');

viewworks.addEventListener('mouseover',()=>{
    viewworks.style.transform='scale(1.02)';
    i2.style.transform='scale(1.1)';
});
viewworks.addEventListener('mouseout', ()=>{
    viewworks.style.transform='scale(1)';
    i2.style.transform='scale(1)';
});

document.getElementById('scrollIcon').addEventListener('click', function() {
    const target = document.getElementById('main3h33');
    const offset = -50;
    const targetPosition = target.getBoundingClientRect().top + window.pageYOffset - offset;    
    window.scrollTo({
      top: targetPosition,
      behavior: 'smooth'
    });
  });

  document.getElementById('circle22').addEventListener('click', function() {
    const target = document.getElementById('main3h33');
    const offset = -50;
    const targetPosition = target.getBoundingClientRect().top + window.pageYOffset - offset;    
    window.scrollTo({
      top: targetPosition,
      behavior: 'smooth'
    });
  });

const containers=document.querySelectorAll('.cont');
containers.forEach(container =>{
    const hoverText=container.querySelector('.containers-p');
    const i4=container.querySelector('.i4');
    const blackscreen=container.querySelector('.blackscreen');
    container.addEventListener('mouseover', function() {
        hoverText.style.opacity='1';
        hoverText.style.transition='opacity 3s ease';  
        i4.style.borderRadius = '100%';
        i4.style.backgroundColor='white';
        i4.style.transform='scale(2)';  
        blackscreen.style.clipPath='inset(0% 0% 0 0)';
    });
    container.addEventListener('mouseout', function() {
        hoverText.style.opacity='0';
        hoverText.style.transition='opacity 0.7s ease';  
        i4.style.color='black';
        i4.style.transform='scale(1)';
        i4.style.backgroundColor='grey';
        i4.style.bordeRadius='100%';
        blackscreen.style.clipPath='inset(100% 100% 0 0)';
    });
});

const whatwedo2=document.querySelector('.whatwedo2');
const i3=document.querySelector('.i3');
whatwedo2.addEventListener('mouseover', ()=>{
    whatwedo2.style.transform='scale(1.02)';
    i3.style.transform='scale(1.1)';
});
whatwedo2.addEventListener('mouseout', ()=>{
    whatwedo2.style.transform='scale(1)';
    i3.style.transform='scale(1)';
});

const whatwedo3=document.querySelector('.whatwedo3');
const i5=document.querySelector('.i5');
whatwedo3.addEventListener('mouseover',()=>{
    whatwedo3.style.transform='scale(1.02)';
    i5.style.transform='scale(1.1)';
});
whatwedo3.addEventListener('mouseout', ()=>{
    whatwedo3.style.transform='scale(1)';
    i5.style.transform='scale(1)';
});


const cursor = document.querySelector('.cursor');
const follower = document.querySelector('.follower');

let mouseX = 0;
let mouseY = 0;
let followerX = 0;
let followerY = 0;

document.addEventListener('mousemove', (e) => {
  mouseX = e.clientX;
  mouseY = e.clientY;
  cursor.style.transform = `translate(${mouseX}px, ${mouseY}px)`;
});


window.addEventListener('scroll', ()=>{
    const rect1=whatwedo.getBoundingClientRect();
    const rect2=viewworks.getBoundingClientRect();
    const arrows=document.querySelectorAll('.arrow');
    const rect4=circle1.getBoundingClientRect();
    const rect5=circle2.getBoundingClientRect();
    const rect6=whatwedo2.getBoundingClientRect();
    const rect8=whatwedo3.getBoundingClientRect();
    if(mouseX>=rect1.left && mouseX<=rect1.right && mouseY>rect1.top && mouseY<rect1.bottom){
        whatwedo.style.transform='scale(1.02)';
        i1.style.transform='scale(1.1)';
    }else{
        whatwedo.style.transform='scale(1)';
        i1.style.transform='scale(1)';
    }

    if(mouseX>=rect2.left && mouseX<=rect2.right && mouseY>rect2.top && mouseY<rect2.bottom){
        viewworks.style.transform='scale(1.02)';
        i2.style.transform='scale(1.1)';
    }else{
        viewworks.style.transform='scale(1)';
        i2.style.transform='scale(1)';
    }

    arrows.forEach(targetElement=>{
        const rect3=targetElement.getBoundingClientRect();
    if(mouseX>=rect3.left && mouseX<=rect3.right && mouseY>rect3.top && mouseY<rect3.bottom){
       targetElement.style.transform='scale(1.1)';
    }else{
        targetElement.style.transform='scale(1)';
    }
    });

    if(mouseX>=rect4.left && mouseX<=rect4.right && mouseY>rect4.top && mouseY<rect4.bottom){
        circle2.style.transform='scale(1.2)';
    }else{
        circle2.style.transform='scale(1)';
    }
    if(mouseX>=rect5.left && mouseX<=rect5.right && mouseY>rect5.top && mouseY<rect5.bottom){
        circle2.style.transform='scale(1.2)';
    }else{
        circle2.style.transform='scale(1)';
    }

    if(mouseX>=rect6.left && mouseX<=rect6.right && mouseY>rect6.top && mouseY<rect6.bottom){
        whatwedo2.style.transform='scale(1.02)';
        i3.style.transform='scale(1.1)';
    }else{
        whatwedo2.style.transform='scale(1)';
        i3.style.transform='scale(1)';
    }

    containers.forEach(container =>{
        const rect7=container.getBoundingClientRect();
        const hoverText=container.querySelector('.containers-p');
        const i4=container.querySelector('.i4');
        const blackscreen=container.querySelector('.blackscreen');
        if(mouseX>=rect7.left && mouseX<=rect7.right && mouseY>rect7.top && mouseY<rect7.bottom){
        hoverText.style.opacity='1';
        hoverText.style.transition='opacity 3s ease';  
        i4.style.borderRadius = '100%';
        i4.style.backgroundColor='white';
        i4.style.transform='scale(2)';
        blackscreen.style.clipPath='inset(0% 0% 0 0)';
        }else{
        hoverText.style.opacity='0';
        hoverText.style.transition='opacity 0.7s ease';  
        i4.style.color='black';
        i4.style.transform='scale(1)';
        i4.style.backgroundColor='grey';
        i4.style.bordeRadius='100%';
        blackscreen.style.clipPath='inset(100% 100% 0 0)';
        }
        });
    if(mouseX>=rect8.left && mouseX<=rect8.right && mouseY>rect8.top && mouseY<rect8.bottom){
        whatwedo3.style.transform='scale(1.02)';
        i5.style.transform='scale(1.1)';
    }else{
        whatwedo3.style.transform='scale(1)';
        i5.style.transform='scale(1)';
        }
});

function moveFollower() {
  followerX += (mouseX - followerX) * 0.1;
  followerY += (mouseY - followerY) * 0.1;
  follower.style.transform = `translate(${followerX}px, ${followerY}px)`;
  requestAnimationFrame(moveFollower);
}

requestAnimationFrame(moveFollower);

