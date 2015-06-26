(function() {
	DD = typeof DD === 'undefined' ? function(){} : DD;
	
	function DotNode(nodeId, attributes) {
		this.id = nodeId;
		this.attributes = attributes;
		this.links = [];

		this.setLink = function(node) {
			this.links.push(node);
		};
	}
	
	function DotEdge(fromNode, toNode, attributes) {
		this.fromNode = fromNode;
		this.toNode = toNode;
		this.attributes = attributes;

		fromNode.setLink(toNode);
		toNode.setLink(fromNode);
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
	var drawer = new Drawer();

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

		drawer.calculatePositions(graph.nodes, {
			x: 1,
			y: 1
		});
		drawer.drawGraph(canvas, graph);
	};

	function Drawer() {

		var xStep = 90, yStep = 90;
		var radius = 28;

		this.calculatePositions = function(nodes, startingSlot) {
			var self = this;

			nodes.forEach(function(node, index) {
				if (typeof node.position === 'undefined') {
					node.position = {
						x: startingSlot.x * xStep,
						y: startingSlot.y * yStep
					};

					self.calculatePositions(node.links, {
						x: startingSlot.x,
						y: startingSlot.y + 1
					});

					startingSlot.x += 1;
				}
			});
		};

		this.drawGraph = function(canvas, graph) {
			var ctx = canvas.getContext('2d');

			graph.edges.forEach(function(edge) {
				drawEdge(ctx, edge);
			});

			graph.nodes.forEach(function(node) {
				drawNode(ctx, node);
			});
		};

		function drawNode(ctx, node) {
			var centerX = node.position.x, 
				centerY = node.position.y;
			
			var style = node.attributes.style || 'solid',
				label = node.attributes.label || node.id;
			
			setLineStyle(ctx, style);

			ctx.beginPath();
			ctx.arc(centerX, centerY, radius, 0, 2 * Math.PI, false);
			ctx.fillStyle = 'white';
			ctx.lineWidth = 2;
			ctx.fill();
			ctx.stroke();

			ctx.textAlign = 'center';
  			ctx.fillStyle = 'black';
			ctx.font = "15px serif";
			ctx.fillText(label, centerX, centerY + 5);
		}

		function drawEdge(ctx, edge) {
			var centerX1 = edge.fromNode.position.x,
				centerY1 = edge.fromNode.position.y,
				centerX2 = edge.toNode.position.x,
				centerY2 = edge.toNode.position.y;
			
			var style = edge.attributes.style || 'solid',
				label = edge.attributes.label;

			setLineStyle(ctx, style);

			ctx.beginPath();
			ctx.moveTo(centerX1, centerY1);
			ctx.lineTo(centerX2, centerY2);
			ctx.lineWidth = 2;
			ctx.stroke();

			if (label) {
				ctx.font = "15px serif";
				ctx.fillText(label, 4 + (centerX1 + centerX2) / 2, 4 + (centerY1 + centerY2) / 2);
			}
		}

		function setLineStyle(ctx, style) {
			switch (style) {
				case 'dashed':
					ctx.setLineDash([5]); 
					break;
				case 'dotted':
					ctx.setLineDash([1, 2]);
					break;
				case 'solid':
					ctx.setLineDash([0]);
					break;
			}
		}
	}
})();