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
	
	function DotGraph(id) {

		this.id = id;
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

		this.addCluster = function(cluster) {
			var self = this;
			cluster.nodes.forEach(function(node) {
				self.nodes.push(node);
			});

			cluster.edges.forEach(function(edge) {
				self.edges.push(edge);
			});

			var clusterNode = new DotNode(cluster.id, {});
			clusterNode.cluster = true;
			clusterNode.nodes = cluster.nodes;

			this.nodes.push(clusterNode)
		};
	}
		
	var drawingCanvasId;
	var drawer = new Drawer();

	DD.createGraph = function(id) {
		return new DotGraph(id);
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

		var canvas = document.getElementById(drawingCanvasId);

		setCanvasHandler(canvas, graph, drawer);
	};

	function setCanvasHandler(canvas, graph, drawer) {
		var selectedNode = undefined;

		canvas.addEventListener('mousedown', function(e){
			var selectedNodes = graph.nodes.filter(function(node) {
				if (node.cluster)
					return false;

				var x = e.offsetX,
					y = e.offsetY,
					nodeX = node.position.x,
					nodeY = node.position.y,
					radius = node.radius;

				return x <= nodeX + radius &&  nodeX - radius <= x
						&& y <= nodeY + radius &&  nodeY - radius <= y;
			});

			if (selectedNodes.length > 0)
				selectedNode = selectedNodes[0];
		});

		canvas.addEventListener('mousemove', function(e) {
			if (!selectedNode)
				return;

			selectedNode.position.x = e.offsetX;
			selectedNode.position.y = e.offsetY;

			drawer.drawGraph(canvas, graph);
		});

		canvas.addEventListener('mouseup', function(e) {
			selectedNode = undefined;
		});
	}

	function Drawer() {

		var xStep = 90, yStep = 90;
		var defaultRadius = 28;

		this.calculatePositions = function(nodes, startingSlot) {
			var self = this;

			nodes.forEach(function(node, index) {
				if (typeof node.position === 'undefined' && !node.cluster) {
					node.position = {
						x: startingSlot.x * xStep,
						y: startingSlot.y * yStep
					};
					node.radius = defaultRadius;

					self.calculatePositions(node.links, {
						x: startingSlot.x,
						y: startingSlot.y + 1
					});

					startingSlot.x += 1;
				}
			});

			nodes.forEach(function(node) {
				if (node.cluster)
					node.position = node.nodes[0].position;
			});
		};

		this.drawGraph = function(canvas, graph) {
			var ctx = canvas.getContext('2d');

			ctx.clearRect(0, 0, canvas.width, canvas.height);

			graph.edges.forEach(function(edge) {
				if (edge.fromNode.cluster || edge.toNode.cluster) 
					drawEdge(ctx, edge);
			});

			graph.nodes.forEach(function(node) {
				if (node.cluster) {
					var rect = getRectangle(node.nodes);

					ctx.beginPath();
					ctx.fillStyle = "#000000";
					ctx.rect(rect.x, rect.y, rect.w, rect.h);
					ctx.stroke();
					ctx.fillStyle = "#FFFFFF";
					ctx.fillRect(rect.x, rect.y, rect.w, rect.h);
				}
			});

			graph.edges.forEach(function(edge) {
				if (!edge.fromNode.cluster && !edge.toNode.cluster) 
					drawEdge(ctx, edge);
			});

			graph.nodes.forEach(function(node) {
				if (!node.cluster)
					drawNode(ctx, node);
			});

		};

		function getRectangle(nodes) {
			var min = {x:nodes[0].position.x, y: nodes[0].position.y},
				max = {x:nodes[0].position.x, y: nodes[0].position.y};


			for (var i = 0; i < nodes.length; i++) {
				if (nodes[i].position.x < min.x)
					min.x = nodes[i].position.x;

				if (nodes[i].position.y < min.y)
					min.y = nodes[i].position.y;

				if (nodes[i].position.x > max.x)
					max.x = nodes[i].position.x;

				if (nodes[i].position.y > max.y)
					max.y = nodes[i].position.y;
			}

			return {
				x: min.x - 40,
				y: min.y - 40,
				w: max.x - min.x + 80,
				h: max.y - min.y + 80
			};
		}

		function drawNode(ctx, node) {
			var centerX = node.position.x, 
				centerY = node.position.y;
			
			var style = node.attributes.style || 'solid',
				label = node.attributes.label || node.id;
			
			setLineStyle(ctx, style);

			ctx.beginPath();
			ctx.arc(centerX, centerY, node.radius, 0, 2 * Math.PI, false);
			ctx.fillStyle = 'white';
			ctx.lineWidth = 2;
			ctx.stroke();
			ctx.fill();

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
				ctx.textAlign = 'left';
				ctx.font = "15px serif";
				ctx.fillText(label, (centerX1 + centerX2) / 2, (centerY1 + centerY2) / 2);
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