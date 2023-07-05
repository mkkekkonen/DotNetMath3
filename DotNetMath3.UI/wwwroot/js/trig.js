function runIndex() {
    var AXES_DIM = 480;
    var CIRCLE_DIM = 400;
    var CIRCLE_OFFSET = 40;
    var MARGIN = 100;
    var CENTER = 240;

    var black = '#000';
    var red = '#f00';
    var green = '#090'

    function scaleAndTranslate(val) {
        return (val * 200) + (AXES_DIM / 2);
    }

    function degToRad(deg) {
        return deg * (Math.PI / 180);
    }

    function roundTo2Places(n) {
        return Math.round((n + Number.EPSILON) * 100) / 100;
    }

    var stage = new Konva.Stage({
        container: 'anim',
        width: AXES_DIM + MARGIN,
        height: AXES_DIM + MARGIN,
    });

    var layer = new Konva.Layer();

    var unitCircle = new Konva.Circle({
        x: AXES_DIM / 2,
        y: AXES_DIM / 2,
        radius: CIRCLE_DIM / 2,
        stroke: black,
        strokeWidth: 1,
    });

    var rotatingRadius = new Konva.Line({
        points: [CENTER, CENTER, CENTER + (CIRCLE_DIM / 2), CENTER],
        x: CENTER,
        y: CENTER,
        stroke: black,
        strokeWidth: 3,
        offset: {
            x: CENTER,
            y: CENTER,
        },
    });

    var arcDot = new Konva.Circle({
        x: CENTER + (CIRCLE_DIM / 2),
        y: CENTER,
        radius: 5,
        stroke: black,
        fill: black,
        strokeWidth: 1,
    });

    var dashLength = 5;
    var dashGap = 2;

    var xAxisY = AXES_DIM / 2;
    var xAxis = new Konva.Line({
        points: [0, xAxisY, AXES_DIM, xAxisY],
        stroke: black,
        strokeWidth: 1,
        dash: [dashLength, dashGap],
    });

    var yAxisX = AXES_DIM / 2;
    var yAxis = new Konva.Line({
        points: [yAxisX, 0, yAxisX, AXES_DIM],
        stroke: black,
        strokeWidth: 1,
        dash: [dashLength, dashGap],
    });

    var sineX = AXES_DIM + (MARGIN / 2);
    var sineLine = new Konva.Line({
        points: [sineX, CIRCLE_OFFSET, sineX, AXES_DIM - CIRCLE_OFFSET],
        stroke: red,
        strokeWidth: 1,
    });

    var sine = 0;
    var sineDot = new Konva.Circle({
        x: sineX,
        y: scaleAndTranslate(sine),
        radius: 5,
        strokeWidth: 1,
        stroke: red,
        fill: red,
    });

    var cosineY = AXES_DIM + (MARGIN / 2);
    var cosineLine = new Konva.Line({
        points: [CIRCLE_OFFSET, cosineY, AXES_DIM - CIRCLE_OFFSET, cosineY],
        stroke: green,
        strokeWidth: 1,
    });

    var cosine = 0;
    var cosineDot = new Konva.Circle({
        x: scaleAndTranslate(cosine),
        y: cosineY,
        radius: 5,
        strokeWidth: 1,
        stroke: green,
        fill: green,
    });

    layer.add(unitCircle);
    layer.add(rotatingRadius);
    layer.add(arcDot);

    layer.add(xAxis);
    layer.add(yAxis);

    layer.add(sineLine);
    layer.add(sineDot);
    layer.add(cosineLine);
    layer.add(cosineDot);

    stage.add(layer);

    var speed = -90;
    var angle = 0;
    var animation = new Konva.Animation(function (frame) {
        var angleDiff = (frame.timeDiff * speed) / 1000;

        angle += angleDiff;
        angle %= 360;
        rotatingRadius.rotation(angle);

        const sine = Math.sin(degToRad(angle));
        const cosine = Math.cos(degToRad(angle));

        const x = scaleAndTranslate(cosine);
        const y = scaleAndTranslate(sine);

        arcDot.x(x);
        arcDot.y(y);

        sineDot.y(y);
        cosineDot.x(x);

        document.getElementById('sine').innerText = roundTo2Places(-sine); // Y axis reversed
        document.getElementById('cosine').innerText = roundTo2Places(cosine);
        document.getElementById('angle').innerText = (roundTo2Places(-angle) + '°');
    });

    animation.start();
}