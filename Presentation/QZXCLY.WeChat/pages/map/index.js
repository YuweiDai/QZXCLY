// pages/map/index.js
var app = getApp();
var p = null;
var rpx=2;
Page({

  /**
   * 页面的初始数据
   */
  data: {
    selectedSpot:0,
    markers:[],
    spots:[
      {
        id: 0,
        title: "康养衢江·隐柿东坪",
        address: "衢州市衢江区峡川镇驻地峡口村东北部13公里",
        tags: ["AAA级景区", "柿子节", "康养"],
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/j3.jpg",        
        latitude: 29.1851002329,
        longitude: 119.0333890915,
        service_list: [
          {
            id: 0,
            title: "停车场",
            descriptions: "",
            t:"parking",
            lat: 29.1850627657,
            lon: 119.0287113190
          },
          {
            id: 1,
            title: "公共厕所",
            descriptions: "",
            t:"wc",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/map/wc.png",
            lat: 29.1851939009,
            lon: 119.0326166153
          }
        ],
        play_list: [
          {
            id: 0,
            title: "千年古道",
            descriptions: "东坪千年古道位于衢州市峡川镇境内，距市区40公里。从东坪山脚下起，一条总长1500米，宽2米，共1144级的青石板古道蜿蜒盘曲伸向山顶，随着岁月的流逝，村民的脚步磨去了古道青石块的棱角而变得光滑。这就是相传具有1300多年历史的唐朝古道。",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/dpgd_logo.jpg",
            audio: "http://sc1.111ttt.cn/2017/1/11/11/304112004168.mp3",
            panorama: "http://qzch.qz.gov.cn/qzxcly/resources/panoramas/index.html",
            latitude: 29.1850627657,
            longitude: 119.0282177925       
          },
          {
            id: 1,
            title: "山涧溪水",
            descriptions: "景点文字介绍",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l2.png",
            audio: "http://sc1.111ttt.cn/2017/1/11/11/304112002493.mp3",
            latitude: 29.1836015342, 
            longitude: 119.0318441391              
          },
          {
            id: 2,
            title: "古树景观",
            descriptions: "景点文字介绍",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l1.jpg",
            audio: "http://sc1.111ttt.cn/2017/1/11/11/304112003137.mp3",
            latitude: 29.1820653453, 
            longitude: 119.0363502502             
          },
          {
            id: 3,
            title: "古树群",
            descriptions: "景点文字介绍",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l2.png",
            latitude: 29.1853063023,
            longitude: 119.0312004089          
          },
          {
            id: 4,
            title: "火烧红枫",
            descriptions: "景点文字介绍",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l2.png",
            latitude: 29.1846412588, 
            longitude: 119.0320372581              
          },
        ],
        eat_list: [
          {
            id: 13,
            title: "隐柿东坪",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/ysdp.bmp",
            latitude: 29.1868611760, 
            longitude: 119.0340542793              
          },
          {
            id: 0,
            title: "红枫堂",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/hft.bmp",
            latitude: 29.1855030045, 
            longitude: 119.0349018574              
          },
          {
            id: 1,
            title: "荷塘轩",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/htx.bmp",
            latitude: 29.1879851663, 
            longitude: 119.0331745148            
          },
          {
            id: 2,
            title: "古道居",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/gdj.bmp",
            latitude: 29.1849597308, 
            longitude: 119.0328419209             
          },
          {
            id: 3,
            title: "东坪院",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/dpy.bmp",
            latitude: 29.1858027404,
            longitude: 119.0343546867           
          }
        ],
        live_list: [
          {
            id: 13,
            title: "隐柿东坪",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/ysdp.bmp",
            latitude: 29.1868611760,
            longitude: 119.0340542793                  
          },
          {
            id: 0,
            title: "红枫堂",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/hft.bmp",
            latitude: 29.1854468039, 
            longitude: 119.0333569050           
          },
          {
            id: 2,
            title: "古道居",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/gdj.bmp",
            latitude: 29.1843508863, 
            longitude: 119.0319621563                    
          },
          {
            id: 4,
            title: "华兰阁",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/hlg.bmp",
            latitude: 29.1853344026, 
            longitude: 119.0351378918               
          },
          {
            id: 6,
            title: "米兰农庄",
            descriptions: "衢州市峡川镇东坪村",
            logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/mlnz.bmp",
            latitude: 29.1864771431, 
            longitude: 119.0333247185                
          }         
        ]                   
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
    allSpotMarkers:[],
    currentSpot: null,
    lon: 118.62230954575,
    lat: 28.906426976353366,
    level: 12,
    showSingelSpot: false, 
    filterType: 'service',
    points:[],
    currentControls: [],
    currentRegionPoints:[],
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
            var currentLevel = res.scale;
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
            var currentLevel = res.scale;
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
          padding: [30],
          points: p.data.currentRegionPoints
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
        //controls.splice(3, 1);
        console.log(200 / rpx);
        controls[0].position.top += 200 / rpx;        
        controls[1].position.top += 200 / rpx;
        controls[2].position.top += 200 / rpx;
        controls[3].position.top += 200 / rpx;
 
        wx.setNavigationBarTitle({
          title: spot.title
        });

        p.setSubMarkers(spot, p.data.filterType);
      
        this.setData({        
          currentSpot:spot,
          showSingelSpot: true,
          currentControls: controls
        });
        break;    
      case "play":
      wx.navigateTo({
        url: '../index/spot_play',
      });
        break;
      case "eat":
        wx.navigateTo({
          url: '../index/spot_eat',
        });      
        break;
      case "live":
        wx.navigateTo({
          url: '../index/spot_live',
        });      
        break;                
    }    
  },
  returnToAll: function (event) {

    var controls = p.data.allControls;
    controls[0].position.top -= 200 / rpx;
    controls[1].position.top -= 200 / rpx;
    controls[2].position.top -= 200 / rpx;
    controls[3].position.top -= 200 / rpx;

    var points = [];
    var spots = [];
    for (var index in p.data.spots) {
      var spot = p.data.spots[index];
      points.push({ latitude: spot.latitude, longitude: spot.longitude });

      spot.width = 30 * rpx;
      spot.height = 30 * rpx;
      spot.iconPath = "../../resources/images/map/spot.png";
      spot.id = "spot_" + spot.id;
      spots.push(spot);
    }
    this.mapCtx.includePoints({
      padding: [30],
      points: points
    }); 

    wx.setNavigationBarTitle({
      title: "衢州市乡村旅游电子地图"
    });
    this.setData({
      markers: spots,
      currentSpot:null,
      currentControls: controls,
      showSingelSpot: false,
    });
  },
  tapFilter: function (e) {

    var currentSpot = p.data.currentSpot;
    p.setSubMarkers(currentSpot, e.target.dataset.id);

    this.setData({
      filterType: e.target.dataset.id,
    });
  }, 

  setSubMarkers:function(currentSpot,filterType){
    var subMarkers = [];
    var newRegionPoints = [];
    switch (filterType) {
      case 'service':       
        currentSpot.service_list.forEach(function (item) {
          subMarkers.push({
            id: "service_" + item.id,
            iconPath: "../../resources/images/map/" + item.t + ".png",
            latitude: item.lat,
            longitude: item.lon,
            height: 80 / rpx,
            width: 80 / rpx,
            anchor: { x: 0.5, y: 0.5 }
          });

          newRegionPoints.push({ latitude: item.lat, longitude: item.lon });
        });
        break;
      case 'play':
        currentSpot.play_list.forEach(function (item) {

          subMarkers.push({
            id: "play_" + item.id,
            title: item.title,
            iconPath: "../../resources/images/map/t.png",
            latitude: item.latitude,
            longitude: item.longitude,
            height: 80 / rpx,
            width: 80 / rpx,
            anchor: { x: 0.5, y: 0.5 }
          });

          newRegionPoints.push({ latitude: item.latitude, longitude: item.longitude });
        });
        break;
      case 'eat':
        currentSpot.eat_list.forEach(function (item) {

          subMarkers.push({
            id: "eat_" + item.id,
            title: item.title,
            iconPath: "../../resources/images/map/eat.png",
            latitude: item.latitude,
            longitude: item.longitude,
            height: 80 / rpx,
            width: 80 / rpx,
            anchor: { x: 0.5, y: 0.5 }
          });

          newRegionPoints.push({ latitude: item.latitude, longitude: item.longitude });
        });      
        break;
      case 'live':
        currentSpot.live_list.forEach(function (item) {

          subMarkers.push({
            id: "live_" + item.id,
            title: item.title,
            iconPath: "../../resources/images/map/t.png",
            latitude: item.latitude,
            longitude: item.longitude,
            height: 80 / rpx,
            width: 80 / rpx,
            anchor: { x: 0.5, y: 0.5 }
          });

          newRegionPoints.push({ latitude: item.latitude, longitude: item.longitude });
        });      
        break;
    }

    this.mapCtx.includePoints({
      padding: [30],
      points: newRegionPoints
    });

    console.log(subMarkers);

    this.setData({
      markers:subMarkers,    
      currentRegionPoints: newRegionPoints
    });
  },

  showImages:function(event)
  {
    wx.previewImage({
      current: "http://img8.blog.eastmoney.com/zl/zltkg/201505/20150530075406190.jpg",
      urls: ["http://img8.blog.eastmoney.com/zl/zltkg/201505/20150530075406190.jpg", "http://s9.rr.itc.cn/r/wapChange/20169_12_15/a29q5o2502122756352.jpg", "http://img2.niwota.com/album/images/2015-07-15/1436925113922-club.jpg"],
      success: function(res) {},
      fail: function(res) {},
      complete: function(res) {},
    })
  },

  //统一导航函数
  nav:function(event){
    var url=event.currentTarget.dataset.url;
    wx.navigateTo({
      url: url,
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    if(p==null) p=this;
    this.mapCtx = wx.createMapContext('map');
    rpx =750/ app.globalData.systemInfo.screenWidth ;
    
    var controlSize = 20 * rpx;
    console.log(controlSize);
    //设置地图控件
    var controls = [
      { id: "locateBtn", iconPath: '../../resources/images/map/locate.png', position: { left: controlSize / 3, top:  3.3 * controlSize, width: controlSize, height: controlSize},clickable: true},
      { id: "zoomInBtn", iconPath: '../../resources/images/map/zoomIn.png', position: { left: controlSize / 3, top: controlSize/3, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomOutBtn", iconPath: '../../resources/images/map/zoomOut.png', position: { left: controlSize / 3, top: controlSize / 3 + controlSize, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomAllBtn", iconPath: '../../resources/images/map/zoomAll.png', position: { left: controlSize / 3, top: 2.3 * controlSize, width: controlSize, height: controlSize }, clickable: true }];

    //设置初始景点图标及原始缩放位置
    var points = [];
    var spots=[];
    for (var index in p.data.spots) {
      var spot = p.data.spots[index];
      points.push({ latitude: spot.latitude, longitude: spot.longitude });

      spot.width = 30 * rpx;
      spot.height = 30 * rpx;
      spot.iconPath = "../../resources/images/map/spot.png";
      spot.id="spot_"+spot.id;
      spots.push(spot);
    } 
    this.mapCtx.includePoints({
      padding: [30],
      points: points
    }); 

    //设置数据
    p.setData({
      markers:spots,
      allSpotMarkers:spots,
      currentRegionPoints: points,
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