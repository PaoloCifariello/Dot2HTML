(function() {
	DD = typeof DD === 'undefined' ? function(){} : DD;
	
	function Node(nodeId, attributes) {
		this.id = nodeId;
		this.attributes = attributes;
	}
	
	function Edge(fromNode, toNode, attributes) {
		this.fromNode = fromNode;
		this.toNode = toNode;
		this.attributes = attributes;
	}
	
	function Graph() {
		
		this.nodes = [];
		this.edges = [];
		
		this.addNode = function(nodeId, attributes) {
			this.nodes.push(new Node(nodeId, attributes));
		};
		
		this.addEdge = function(fromNodeId, toNodeId, attributes) {
			
			var fromNode = this.nodes.filter(function(node) {
				return node.id === fromNodeId;
			})[0];

			var toNode = this.nodes.filter(function(node) {
				return node.id === toNodeId;
			})[0];
			
			if (typeof fromNode === 'undefined')
				fromNode = new Node(fromNodeId, {});
				
			if (typeof toNode === 'undefined')
				toNode = new Node(toNodeId, {});
				
			this.edges.push(new Edge(fromNode, toNode, attributes));
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

		var canvas = document.getElementById(canvasId);
	};
})();