var onMove, onDrag;
var redipsInit;

// redips initialization
redipsInit = function () {
    var rdg = REDIPS.drag;	// reference to the REDIPS.drag class   
    // DIV container initialization
    rdg.init('drag1');
    rdg.init('drag2');
    rdg.dropMode = 'shift';
    rdg.drop_option = 'single';
    rdg.shift.mode = 'vertical2';
};

// add onload event listener
if (window.addEventListener) {
    window.addEventListener('load', redipsInit, false);
}
else if (window.attachEvent) {
    window.attachEvent('onload', redipsInit);
}


// after page is loaded, initialize DIV elements inside table
window.onload = function () {
    // reference to the REDIPS.drag library
    var rd = REDIPS.drag;
    // initialization
    rd.init();
    // set hover color
    rd.hover.colorTd = '#E7C7B2';
    // set drop option to 'shift'
    rd.dropMode = 'shift';
    rd.drop_option = 'single';
    // set shift mode to vertical2
    rd.shift.mode = 'vertical2';
    // enable shift animation
    rd.shift.animation = true;
    // set animation loop pause
    rd.animation.pause = 20;
    // display action in the message line (list of all event handlers can be found at the drag.js bottom)
    rd.event.clicked = function () { };
    rd.event.moved = function () { };
    rd.event.notMoved = function () { };
    rd.event.dropped = function () { };
}


