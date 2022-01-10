
export function ScrollToY(element) {
    let position = element.getBoundingClientRect();
    let y = position.top;
    return window.scrollTo(0, y);
}