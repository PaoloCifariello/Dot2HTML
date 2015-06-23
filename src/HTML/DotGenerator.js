(function() {
	DD = typeof DD === 'undefined' ? function(){} : DD;
	
	function Graph() {
		this.addNode = function(nodeId) {
			
		};
		
		this.addEdge = function(fromNode, toNode) {
			
		};
	}
	
	var drawingCanvasId;
	
	DD.createGraph = function() {
		return new Graph();
	};
	
	DD.setDrawingCanvas = function(canvasId) {
		drawingCanvasId = canvasId;	
	};
	
	DD.drawGraph = function(graph, canvasId) {
		canvasId = typeof canvasId === 'undefined' ? drawingCanvasId : canvasId;
	};
})();