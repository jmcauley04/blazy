
export function scrollToY(element) {
    let main = document.getElementsByTagName('main');
    let position = element.getBoundingClientRect();
    let y = position.top;
    return window.scrollTo(0, y);
}