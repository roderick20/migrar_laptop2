document.addEventListener("DOMContentLoaded", (() => {
    if (document.getElementsByClassName("js-simplebar")[0]) {        
        const e = document.getElementsByClassName("sidebar")[0];
        document.getElementsByClassName("sidebar-toggle")[0].addEventListener("click", (() => {
            e.classList.toggle("collapsed"),
            e.addEventListener("transitionend", (() => { window.dispatchEvent(new Event("resize")) }))
        }))
    }
}));