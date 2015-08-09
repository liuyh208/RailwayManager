dojo.require("esri.map");
dojo.require("esri.dijit.Popup");
dojo.require("esri.dijit.PopupTemplate");
dojo.require("esri.graphic");
dojo.require("esri.dijit.Measurement");
dojo.require("esri.dijit.Scalebar");
dojo.require("dojo.Evented");
dojo.require("dojo/on");
dojo.require("esri.tasks.geometry");

dojo.declare("GasMap.MapX", dojo.Evented,
{
    myDivMap: null,
    mydivXy: null,
    myMap: null,
    gLayer: null,
    glineLayer: null,
    arrayPathStyle: null,
    layerTable: null,
    defaultLayer: 'default',
    isGetXY: false,
    fullExtent: null,
    xyKey: null,
    measurementPanel: null,
    layerNames: null,
    funGetXY: null,
    serviceIP: "192.168.100.207",
    serviceName: "beijing",
    gemetryService: null,
    constructor: function(divMap, divInfo) {
        this.myDivMap = divMap;
        this.mydivXy = divInfo;
        this.arrayPathStyle = new Array();
        this.layerTable = new HashTable();
        this.layerNames = new Array();

        esri.config.defaults.io.proxyUrl = "/proxy";
        esri.config.defaults.io.alwaysUseProxy = false;

    },
    //获得图层
    getLayer: function(layername) {
        if (layername != undefined && layername != "") {
            var layer = this.myMap.getLayer(layername);
            if (layer == null) {
                layer = new esri.layers.GraphicsLayer();
                layer.id = layername;
                this.layerNames.push(layername);
                this.myMap.addLayer(layer);
            }
            return layer;
        }

        return this.gLayer;
    },

    //测量
    Measurement: function(divMeasurement) {
        this.measurementPanel = new esri.dijit.Measurement({
            map: this.myMap,
            defaultAreaUnit: esri.Units.SQUARE_KILOMETERS,
            defaultLengthUnit: esri.Units.KILOMETERS
        }, dojo.byId(divMeasurement));
        this.measurementPanel.startup();
        dojo.connect(this.measurementPanel, "onMeasureEnd", function(activeTool, geometry) {
            this.setTool(activeTool, false);
        });
    },

    init2: function() {
        this.myMap = new esri.Map(this.myDivMap,
        {
            center: [108.7373, 34.2428],
            zoom: 4,


            sliderStyle: "large",
            logo: false
        });
        var me = this;
        var myTiledMapServiceLayer = new esri.layers.ArcGISTiledMapServiceLayer("http://cache1.arcgisonline.cn/ArcGIS/rest/services/ChinaOnlineCommunityENG/MapServer");
        //map.addLayer(layer);
        //var myTiledMapServiceLayer = new esri.layers.ArcGISTiledMapServiceLayer("http://" + this.serviceIP + "/ArcGIS/rest/services/" + this.serviceName + "/MapServer");

        myTiledMapServiceLayer.id = "myBaseMap";
        this.myMap.addLayer(myTiledMapServiceLayer);
        //this.gemetryService = new esri.tasks.GeometryService("http://" + this.serviceIP + "/ArcGIS/rest/services/Geometry/GeometryServer");


        dojo.connect(this.gemetryService, "onBufferComplete", function(geometries) {
            var layer = me.getLayer("bufferlayer");
            me.showBuffer(geometries, layer);
        });

        dojo.connect(this.myMap, "onMouseMove", function(evt) {
            if (me.mydivXy != undefined && me.mydivXy != "") {
                var mp = evt.mapPoint;
                dojo.byId(me.mydivXy).innerHTML = mp.x.toFixed(6) + ", " + mp.y.toFixed(6);
            }
        });

        dojo.connect(this.myMap, "onClick", function(evt) {
            if (me.isGetXY) {
                var pt = evt.mapPoint;
                me.isGetXY = false;
                me.clear("");
                me.addPointNoLine(pt, '../images/map/poi1.png', "", 21, 29);
                //me.emit("OnGetXY", me.xyKey, pt.x, pt.y);
                if (me.funGetXY != null) {
                    me.funGetXY(me.xyKey, pt.x, pt.y);
                }
            }
        });

        dojo.connect(this.myMap, "onLoad", function(theMap) {
            var scalebar = new esri.dijit.Scalebar({
                map: theMap,
                attachTo: "bottom-left",
                scalebarUnit: "metric"
            });
        });

        this.gLayer = new esri.layers.GraphicsLayer();
        this.gLayer.id = "defaultLayer";
        this.layerNames.push("defaultLayer");
        this.myMap.addLayer(this.gLayer);

        var personLayer = new esri.layers.GraphicsLayer();
        personLayer.id = "person";
        this.layerNames.push("person");
        this.myMap.addLayer(personLayer);

        var carLayer = new esri.layers.GraphicsLayer();
        carLayer.id = "car";
        this.layerNames.push("car");
        this.myMap.addLayer(carLayer);

        var wuLayer = new esri.layers.GraphicsLayer();
        wuLayer.id = "wu";
        this.layerNames.push("wu");
        this.myMap.addLayer(wuLayer);

        this.glineLayer = new esri.layers.GraphicsLayer();
        this.glineLayer.id = "Line";
        this.layerNames.push("line");
        this.myMap.addLayer(this.glineLayer);

    },
    loadMap: function() {
        this.init2();
    },


    bufferPoint: function(x, y, bufferWidth) {
        var pt = new esri.geometry.Point(x, y, this.myMap.spatialReference);
        buffer(pt);
    },

    buffer: function(geometry, width) {
        var me = this;
        switch (geometry.type) {
        case "point":
            var symbol = new esri.symbol.SimpleMarkerSymbol(esri.symbol.SimpleMarkerSymbol.STYLE_SQUARE, 10, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1), new dojo.Color([0, 255, 0, 0.25]));
            break;
        case "polyline":
            var symbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASH, new dojo.Color([255, 0, 0]), 1);
            break;
        case "polygon":
            var symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_NONE, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASHDOT, new dojo.Color([255, 0, 0]), 2), new dojo.Color([255, 255, 0, 0.25]));
            break;
        }


        //setup the buffer parameters
        var params = new esri.tasks.BufferParameters();
        params.distances = [width];
        //params.bufferSpatialReference = new esri.SpatialReference({wkid: dojo.byId("bufferSpatialReference").value});

        params.bufferSpatialReference = me.myMap.spatialReference;
        params.outSpatialReference = me.myMap.spatialReference;
        params.unit = esri.tasks.GeometryService.UNIT_METER;

        if (geometry.type === "polygon") {
            //if geometry is a polygon then simplify polygon.  This will make the user drawn polygon topologically correct.
            me.gemetryService.simplify([geometry], function(geometries) {
                params.geometries = geometries;
                me.gemetryService.buffer(params, me.showBuffer);
            });
        } else {
            params.geometries = [geometry];
            me.gemetryService.buffer(params, null, function(er) {
                alert("生成缓冲区错误");
            });
        }
    },

    showBuffer: function(bufferedGeometries, layer) {
        var symbol = new esri.symbol.SimpleFillSymbol(
            esri.symbol.SimpleFillSymbol.STYLE_SOLID,
            new esri.symbol.SimpleLineSymbol(
                esri.symbol.SimpleLineSymbol.STYLE_SOLID,
                new dojo.Color([255, 0, 0, 0.65]), 2
            ),
            new dojo.Color([255, 0, 0, 0.35])
        );

        dojo.forEach(bufferedGeometries, function(geometry) {
            var graphic = new esri.Graphic(geometry, symbol);
            layer.add(graphic);
        });
        //tb.deactivate();
        this.myMap.showZoomSlider();
    },


    //缩放到全图
    zoomFullMap: function() {
        var incidentLayer = this.myMap.getLayer("myBaseMap");
        this.myMap.setExtent(incidentLayer.fullExtent, true);
    },
    //定位
    centerAt: function(x, y, scale) {

        var pt = new esri.geometry.Point(x, y, this.myMap.spatialReference);
        this.center(pt, scale);
    },

    //定位
    center: function(pt, scale) {
        if (scale != undefined && scale > 0) {
            this.myMap.setScale(scale);
        }
        this.myMap.centerAt(pt);
    },

    //设置图层的可见性,返回图层的对象个数
    setLayerVisible: function(layername, visible) {
        var layer = this.getLayer(layername);
        if (visible) {
            layer.show();
        } else {
            layer.hide();
        }
        return layer.graphics.length;
    },

    //添加点
    addPoint: function(x, y, picUrl, attr, infotemp, title, layername, picwidth, picheight) {
        var pt = new esri.geometry.Point(x, y, this.myMap.spatialReference);
        if (picwidth == undefined)
            picwidth = 32;
        if (picheight == undefined)
            picheight = 32;

        var sms = new esri.symbol.PictureMarkerSymbol(picUrl, picwidth, picheight);

        var infoTemplate = new esri.InfoTemplate(title, infotemp);

        graphic = new esri.Graphic(pt, sms, attr, infoTemplate);

        var layer = this.getLayer(layername);
        layer.add(graphic);

        //添加轨迹线
        var polyline = new esri.geometry.Polyline(this.myMap.spatialReference);
        var symbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([100, 60, 0, 0.45]), 2);
        this.arrayPathStyle.push(symbol);

        var arrPoint = new Array();
        arrPoint.push(pt);
        polyline.addPath(arrPoint);

        var graphic2 = new esri.Graphic(polyline, symbol);
        graphic2.hide();
        this.glineLayer.add(graphic2);
        return pt;
    },

    //添加点，不开始划线
    addPointNoLine: function(mpoint, picUrl, layername, picwidth, picheight) {
        if (picwidth == undefined)
            picwidth = 32;
        if (picheight == undefined)
            picheight = 32;

        var sms = new esri.symbol.PictureMarkerSymbol(picUrl, picwidth, picheight);

        graphic = new esri.Graphic(mpoint, sms);

        var layer = this.getLayer(layername);
        layer.add(graphic);
    },
    //删除地图上的点
    clear: function(layername) {
        if (layername == undefined || layername == "") {
            for (var i = 0; i < this.layerNames.length; i++) {
                var layer1 = this.getLayer(this.layerNames[i]);
                layer1.clear();
            }
        } else {
            var layer = this.getLayer(layername);
            layer.clear();
        }
    },
    //更新点位
    //序号，x坐标，y坐标
    updatePoint: function(index, x, y, layername) {
        var layer = this.getLayer(layername);
        if (layer.graphics.length > index && index >= 0) {
            var graphic = layer.graphics[index];
            var pt = graphic.geometry;
            pt.update(x, y);
            graphic.setGeometry(pt);

            var polyline = this.glineLayer.graphics[index].geometry;
            var l = polyline.paths[0].length;
            polyline.insertPoint(0, l, pt);
            this.glineLayer.graphics[index].setGeometry(polyline);
        } else {
            return false;
        }
        layer.redraw();
        return true;
    },
    updateAll: function() {
        if (this.gLayer.graphics.length > 0) {
            for (var i = 0; i < this.gLayer.graphics.length; i++) {
                var graphic = this.gLayer.graphics[i];
                var pt = graphic.geometry;
                var x = pt.x + Math.random() * 0.8;
                var y = pt.y + Math.random() * 0.4;

                pt.update(x, y);
                graphic.setGeometry(pt);
            }
        }
    },

    //移除点
    removePoint: function(index) {
        if (this.gLayer.graphics.length > index && index >= 0) {
            var g = this.gLayer.graphics[index];
            this.gLayer.remove(g);
        }
    },
    //设置轨迹路径是否显示
    setPathVisible: function(index, visible) {
        if (index == undefined) {
            this.glineLayer.clear();
        } else {
            if (this.glineLayer.graphics.length > index && index >= 0) {
                this.glineLayer.graphics[index].show();
            }
        }
        if (this.glineLayer.graphics.length > index && index >= 0) {
            if (visible) {
            } else {
                this.glineLayer.graphics[index].hide();
            }

        }
    },

    //点是否在地图显示范围内
    inMapExtent: function(x, y) {
        var extent = this.myMap.extent;
        if (x > extent.xmin && x < extent.xmax && y > extent.ymin && y < extent.ymax) {
            return true;
        }
        return false;
    },

    //开始获取xy
    startGetXY: function(xykey, fun) {
        this.xyKey = xykey;
        this.isGetXY = true;
        this.funGetXY = fun;
    },

    //删除轨迹路径
    ClearPath: function(index) {

        if (this.glineLayer.graphics.length > index && index >= 0) {
            var g = this.glineLayer.graphics[index];
            this.glineLayer.remove(g);
        }
    },
    //设定路径的样式
    //STYLE_SOLID .The line is solid.
    //STYLE_DASH .The line is made of dashes.
    //STYLE_DOT .The line is made of dots.
    //STYLE_DASHDOTDOT .The line is made of a dash-dot-dot pattern.
    //STYLE_NULL .The line has no symbol.
    //STYLE_DASHDOT. The line is made of a dash-dot pattern.
    //STYLE_SHORTDASH .Line is constructed of a series of short dashes. (Added at v3.4)
    //STYLE_SHORTDOT .Line is constructed of a series of short dots. (Added at v3.4)
    //STYLE_SHORTDASHDOTDOT. Line is constructed of a series of a dash and two dots. (Added at v3.4)
    //STYLE_SHORTDASHDOT.Line is constructed of a dash followed by a dot. (Added at v3.4)
    //STYLE_LONGDASH .Line is constructed of a series of dashes. (Added at v3.4)
    //STYLE_LONGDASHDOT. Line is constructed of a series of short dashes. (Added at v3.4)
    setPathStyle: function(index, width, color, style) {
        if (style == undefined) {
            style = "STYLE_SOLID";
        }
        if (this.arrayPathStyle.length > index && index >= 0) {
            this.arrayPathStyle[index].setColor(new dojo.Color(color));
            if (width < 0) {
                width = 1;
            }
            this.arrayPathStyle[index].setWidth(width);
            this.arrayPathStyle[index].setStyle(style);
            this.glineLayer.graphics[index].setSymbol(this.arrayPathStyle[index]);
        }
    }


});