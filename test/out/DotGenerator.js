(function() {
	DD = typeof DD === 'undefined' ? function(){} : DD;
	
	function DotNode(nodeId, attributes) {
		this.id = nodeId;
		this.attributes = attributes;
	}
	
	function DotEdge(fromNode, toNode, attributes) {
		this.fromNode = fromNode;
		this.toNode = toNode;
		this.attributes = attributes;
	}
	
	function DotGraph() {
		
		this.nodes = [];
		this.edges = [];
		
		this.addNode = function(nodeId, attributes) {
			var node = new DotNode(nodeId, attributes);
			this.nodes.push(node);
			return node;
		};
		
		this.addEdge = function(fromNodeId, toNodeId, attributes) {
			
			var fromNode = this.nodes.filter(function(node) {
				return node.id === fromNodeId;
			})[0];

			var toNode = this.nodes.filter(function(node) {
				return node.id === toNodeId;
			})[0];
			
			if (typeof fromNode === 'undefined')
				fromNode = this.addNode(fromNodeId, {})
				
			if (typeof toNode === 'undefined')
				toNode = this.addNode(toNodeId, {});
				
			this.edges.push(new DotEdge(fromNode, toNode, attributes));
		};
	}
		
	var drawingCanvasId;

	DD.createGraph = function() {
		return new DotGraph();
	};
	
	DD.setDrawingCanvas = function(canvasId) {
		drawingCanvasId = canvasId;	
	};
	
	DD.drawGraph = function(graph, canvasId) {
		canvasId = typeof canvasId === 'undefined' ? drawingCanvasId : canvasId;

		var canvas = document.getElementById(canvasId);

		if (typeof canvas === 'undefined') {
			console.error("Cannot find canvas " + canvasId);
			return;
		}

		var ctx = canvas.getContext('2d');

		graph.nodes.forEach(function(node) {

		});
	};






})();