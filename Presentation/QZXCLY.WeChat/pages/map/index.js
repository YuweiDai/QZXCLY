// pages/map/index.js
var app = getApp();
var p = null;

Page({

  /**
   * 页面的初始数据
   */
  data: {
    selectedSpot:0,
    spots:[
      {
        id: 0,
        title: "康养衢江·隐柿东坪",
        address: "衢州市衢江区峡川镇驻地峡口村东北部13公里",
        tags: ["AAA级景区", "柿子节", "康养"],
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/j3.jpg",        
        latitude: 29.1851002329,
        longitude: 119.0333890915,
      }, 
      {
        id: 1,
        title: "寻觅乡愁 · 桃源七里",
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/j1.jpg", 
        latitude: 29.1484507736,
        longitude: 118.7620890141,
        selected: false
      },
      {
        id: 3,
        title: "七彩长虹",
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/j2.jpg",   
        latitude: 29.2773600000,
        longitude: 118.2112300000,
        selected: false
      },
      {
        id: 4,
        title: "江郎山",
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/j3.jpg",       
        latitude: 28.5341631929,
        longitude: 118.5649681091,
        selected: false    
      }
    ],
    lon: 118.62230954575,
    lat: 28.906426976353366,
    level: 12,
    showSingelSpot: false, filterId: 6,
    points:[],
    service_list: [
      {
        title: "集散中心",
        descriptions: "",
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/map/service.png",
        lat: 29.1851985843, 
        lon: 119.0282231569
      },
      {
        title: "停车场",
        descriptions: "",
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/map/park.png",
        lat: 29.1850627657, 
        lon: 119.0287113190
      },      
      {
        title: "公共厕所",
        descriptions: "",
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/map/wc.png",
        lat: 29.1851939009, 
        lon: 119.0326166153
      },
      {
        title: "应急医疗点",
        descriptions: "",
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/map/medical.png",
        lat: 29.1426786032, 
        lon: 118.9927589893,     
      },
    ],
    currentControls: [],
    allControls: []    
  },
  //控件事件实现
  controltap:function(event)
  {    
    switch(event.controlId)
    {
      case "zoomInBtn":  
        this.mapCtx.getScale ({
          success: function (res) {
            var currentLevel = res.scale+2;
            console.log("current:" + currentLevel);
            currentLevel=currentLevel+1;
            if (currentLevel == 19) currentLevel=18;
            p.setData({
              level: currentLevel,
            });
            console.log("now:" + p.data.level);
          },
          fail:function(res)
          {
            wx.showToast({ title:"获取地图等级失败"});
          }
        });
        break;
      case "zoomOutBtn":
        this.mapCtx.getScale({
          success: function (res) {
            var currentLevel = res.scale + 2;
            console.log("current:" + currentLevel);
            currentLevel--;
            if (currentLevel == 4) currentLevel = 5;
            p.setData({
              level: currentLevel,
            });
          },
          fail: function (res) {
            wx.showToast({ title: "获取地图等级失败" });
          }
        });       
        break;
      case "zoomAllBtn":
        this.mapCtx.includePoints({
          padding: [10],
          points: p.data.points
        });
        break; 
      case "locateBtn":
        this.mapCtx.moveToLocation();
        break;                
    }
  },
  //图钉点击事件
  bindmarkertap: function (event) {

    console.log(event);
    var tappedMarkerId=event.markerId;
    var markerType=tappedMarkerId.split('_')[0];
    switch(markerType)
    {
      case "spot":
        //景区点击事件
        if (p.data.showSingelSpot) return;
        var spot = p.data.spots[0];// markers[event.markerId];

        if (spot.id == undefined || spot.id == null) return;
        var spotMarkers = [];
        // spotMarkers.push(marker);

        //更新空间位置
        var controls = p.data.allControls;
        controls.splice(3, 1);
        console.log(controls);
        controls[0].position.top -= 60 / app.globalData.systemInfo.pixelRatio;        
        controls[1].position.top += 100 / app.globalData.systemInfo.pixelRatio;
        controls[2].position.top += 100 / app.globalData.systemInfo.pixelRatio;

        this.setData({
          showSingelSpot: true,
          lon: spot.longitude,
          lat: spot.latitude,
          level: 18,
          currentControls: controls
        });
        break;        
    }    
  },
  returnToAll: function (event) {

    // var markers = [];
    // for (var index in p.data.spots) {
    //   markers.push({
    //     iconPath: p.data.spots[index].selected ? "/resources/images/map/marker.png" : "/resources/images/map/spot.png",
    //     id: 4,
    //     latitude: p.data.spots[index].latitude,
    //     longitude: p.data.spots[index].longitude,
    //     width: 30,
    //     height: p.data.spots[index].selected ? 45 : 30,
    //   });
    // }
    console.log(p.data.allControls);
    this.setData({
      // lon: 118.8594317436,
      // lat: 28.9702076731,
      // level: 11,
      // markers: markers,
      currentControls:p.data.allControls,
      showSingelSpot: false,
    });
  },
  tapFilter: function (e) {

    this.setData({
      filterId: e.target.dataset.id,
    });
  }, 

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    if(p==null) p=this;
    this.mapCtx = wx.createMapContext('map');
    
    var controlSize =15 * app.globalData.systemInfo.pixelRatio;

    //设置地图控件
    var controls = [
      { id: "locateBtn", iconPath: '../../resources/images/map/locate.png', position: { left: controlSize / 2, top: app.globalData.systemInfo.windowHeight- 2*controlSize, width: controlSize, height: controlSize},clickable: true},
      { id: "zoomInBtn", iconPath: '../../resources/images/map/zoomIn.png', position: { left: controlSize / 2, top: controlSize / 2, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomOutBtn", iconPath: '../../resources/images/map/zoomOut.png', position: { left: controlSize / 2, top: controlSize / 2 + controlSize, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomAllBtn", iconPath: '../../resources/images/map/zoomAll.png', position: { left: controlSize / 2, top: controlSize / 2 + 2 * controlSize + controlSize / 2, width: controlSize, height: controlSize }, clickable: true }];

    //设置初始景点图标及原始缩放位置
    var points = [];
    var spots=[];
    for (var index in p.data.spots) {
      var spot = p.data.spots[index];
      points.push({ latitude: spot.latitude, longitude: spot.longitude });

      spot.width = 30 * app.globalData.systemInfo.pixelRatio;
      spot.height = 30 * app.globalData.systemInfo.pixelRatio;
      spot.iconPath = "../../resources/images/map/spot.png";
      spot.id="spot_"+spot.id;
      spots.push(spot);
    } 


    //设置数据
    p.setData({
      spots: spots,
      points: points,
      currentControls: controls,
      allControls:controls
    });   

    // if (app.globalData.locationDetect)
    // {
    //   page.bindmarkertap({ markerId:1});
    //   app.globalData.locationDetect=false;
    // }
    // else
    // {
    //   this.setData({
    //     mapHeight: app.globalData.systemInfo.windowHeight,
    //     contentHeight: 0,
    //   });

    //   console.log(this.data.mapHeight);
    // }

  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {




  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})