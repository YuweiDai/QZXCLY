// pages/index/map.js
var util = require('../../utils/util');
var page=null;
var app = getApp();
var defaultCallout = {
    padding: 5,
    color: "#ffffff",
    bgColor: "#1296db",
    textAlign: "center",
    display: "ALWAYS",
    borderRadius: 5
};
Page({

  /**
   * 页面的初始数据
   */
  data: {
    suggestion:{
      banners: [
        {
          id: 3,
          img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b1.gif',
          url: '',
          name: '康养衢江·隐柿东坪'
        },
        {
          id: 1,
          img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b2.gif',
          url: '',
          name: '告别午高峰'
        },
        {
          id: 2,
          img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b3.gif',
          url: '',
          name: '金牌好店'
        }
      ],
    },
    map:{
      showModalStatus: false,
      animationData: null,
      drawerMarker: null,
      markerSize: {
        width: 44,
        height: 64
      },
      selectedSpot: 0,
      markers: [],
      polylines: [],
      currentSpot: null,
      lon: 118.62230954575,
      lat: 28.906426976353366,
      level: 12,
      showSingelSpot: false,
      filterType: 'play',
      points: [],
      currentControls: [],
      currentRegionPoints: [],
      allControls: []          
    },
    strategies:[],
    currentTab:"0", // -1 0 1  
    contentHeight:0,
  },

  //切换显示
  setTab:function(event){
    var target=event.currentTarget.dataset.id;

    page.setData({
      currentTab:target
    });
  },

    //刷新获取攻略
  refreshStrategies:function(event)
  {
    var requestPromisified = util.wxPromisify(wx.request);

    requestPromisified({
      url: app.globalData.apiUrl + 'Strategy/Random',
    }).then(function (response) {
      var strategies = response.data;

      strategies.forEach(function (item) {
        item.img = item.img.replace(app.globalData.apiUrl, app.globalData.picturesUrl);
        item.src = app.globalData.resourceUrl + "strategies/" + item.src;
      });
      page.setData({
        strategies: strategies,
      });
    }).catch(function (response) {
      console.log(response);
      wx.showToast({
        title: response//'获取数据失败...',
      })
    });    
  },

  showInMap:function(event){
    var spotId=event.currentTarget.dataset.id;

    if(spotId!=1&&spotId!=2)
    {
      wx.showModal({
        title: '提示',
        content: '该景区数据暂未入库',
        showCancel:false
      });
      return;
    }

    page.setData({
      currentTab:"0"
    });

    page.bindmarkertap({ markerId: 'spot_' + spotId});

  },

  //初始化获取点集合
  initialMarkers: function () {
    //设置初始景点图标及原始缩放位置
    var points = [];
    var spotMarkers = [];
    wx.setNavigationBarTitle({
      title: "衢州市乡村旅游电子地图"
    });
    wx.showLoading({
      title: '数据加载中...',
    });

    //获取景区点
    wx.request({
      url: app.globalData.apiUrl + 'Villages/geo',
      success: function (response) {
        var spots = response.data;

        for (var index in spots) {
          var spot = spots[index];
          points.push({ latitude: spot.latitude, longitude: spot.longitude });

          var callout = {
            content: spot.name,
            padding: defaultCallout.padding,
            color: defaultCallout.color,
            bgColor: defaultCallout.bgColor,
            textAlign: defaultCallout.textAlign,
            display: defaultCallout.display,
            borderRadius: defaultCallout.borderRadius,
          };

          var spotMarker = {
            id: "spot_" + spot.id,
            title: spot.name,
            width: 40 * app.globalData.rpx,
            height: 40 * app.globalData.rpx,
            iconPath: "../../resources/images/map/" + spot.icon + ".png",
            latitude: spot.latitude,
            longitude: spot.longitude,
            callout: callout
          };
          spotMarkers.push(spotMarker);
        }
        page.mapCtx.includePoints({
          padding: [50,50,50,50],
          points: points
        });

        //设置数据
        page.setData({
          'map.markers': spotMarkers,
          'map.currentRegionPoints': points,
        });
      },
      fail: function (response) {
        wx.showToast({
          title: '加载地图点位错误',
        })
      },
      complete: function () {
        wx.hideLoading();
      }
    });
  },

    //控件事件实现
  controltap: function (event) {
      switch (event.controlId) {
          case "zoomInBtn":
              this.mapCtx.getScale({
                  success: function (res) {
                      var currentLevel = res.scale;
                      console.log("current:" + currentLevel);
                      currentLevel = currentLevel + 1;
                      if (currentLevel == 19) currentLevel = 18;
                      page.setData({
                          level: currentLevel,
                      });
                      console.log("now:" + page.data.map.maplevel);
                  },
                  fail: function (res) {
                      wx.showToast({ title: "获取地图等级失败" });
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
                      page.setData({
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
                  padding: [50,50,50,50],
                  points: page.data.map.currentRegionPoints
              });
              break;
          case "locateBtn":
              this.mapCtx.moveToLocation();
              break;
      }
  },
    //图钉点击事件
  bindmarkertap: function (event) {

      var tappedMarkerId = event.markerId;
      var markerType = tappedMarkerId.split('_')[0];
      var markerId = tappedMarkerId.split('_')[1];

      switch (markerType) {
        case "spot":
          //景区点击事件
          if (page.data.map.showSingelSpot) return;
          var spotId = markerId;

          wx.showLoading({
            title: '数据加载中...',
          });

          var currentLon = 0, currentLat = 0;
          wx.getLocation({
            type: "gcj02",
            success: function (res) {
              currentLat = res.latitude
              currentLon = res.longitude
            },
            complete: function () {
              wx.request({
                url: app.globalData.apiUrl + 'villages/geo/' + spotId + '?lon=' + currentLon + '&lat=' + currentLat,
                success: function (response) {
                  var spot = response.data;
                  spot.logo=spot.logo.replace(app.globalData.apiUrl1,app.globalData.picturesUrl);
                  //更新空间位置
                  var controls = page.data.map.allControls;
                  controls[0].position.top += 200 / app.globalData.rpx;
                  controls[1].position.top += 200 / app.globalData.rpx;
                  controls[2].position.top += 200 / app.globalData.rpx;
                  controls[3].position.top += 200 / app.globalData.rpx;

                  wx.setNavigationBarTitle({
                    title: spot.name
                  });

                  page.setSubMarkers(spot, page.data.map.filterType);

                  page.setData({
                    'map.currentSpot': spot,
                    'map.showSingelSpot': true,
                    'map.currentControls': controls
                  });
                },
                fail: function (response) {

                },
                complete: function () {
                  wx.hideLoading();
                }
              });

            }
          });
          break;
        case "play":
        case "eat":
        case "live":

          wx.navigateTo({
            url: 'spot_' + markerType + '?id=' + markerId,
          });

          break;
      }

  },
    //返回所有景点
  returnToAll: function (event) {

      var controls = page.data.map.allControls;
      controls[0].position.top -= 200 / app.globalData.rpx;
      controls[1].position.top -= 200 / app.globalData.rpx;
      controls[2].position.top -= 200 / app.globalData.rpx;
      controls[3].position.top -= 200 / app.globalData.rpx;

      page.initialMarkers();

      page.setData({
          'map.currentSpot': null,
          'map.currentControls': controls,
          'map.showSingelSpot': false,
      });
  },

    //切换吃喝玩乐
  tapFilter: function (e) {

      var currentSpot = page.data.map.currentSpot;
      page.setSubMarkers(currentSpot, e.target.dataset.id);

      var pls = [];
      page.setData({
          'map.filterType': e.target.dataset.id, polylines: pls
      });
  },

    //设置吃喝玩乐
  setSubMarkers: function (currentSpot, filterType) {
      var subMarkers = [];
      var newRegionPoints = [];
      var targetItems = [];

      switch (filterType) {
          case 'service':
              targetItems = currentSpot.services;
              break;
          case 'play':
              targetItems = currentSpot.plays;
              break;
          case 'eat':
              targetItems = currentSpot.eats;
              break;
          case 'live':
              targetItems = currentSpot.lives;
              break;
      }

      targetItems.forEach(function (item) {

        var width = page.data.map.markerSize.width / app.globalData.rpx;
        var height = page.data.map.markerSize.height / app.globalData.rpx;

        if (filterType=="play")
        {
           width = 40 * app.globalData.rpx;
           height = 40 * app.globalData.rpx;          
        }

          subMarkers.push({
              id: filterType + "_" + item.id,
              iconPath: "http://qzch.qz.gov.cn/qzxcly/resources/images/map/spot001.png",//../../resources/images/map/" + item.icon + ".png",
              latitude: item.latitude,
              longitude: item.longitude,
              height: height,
              width: width,
              callout: {
                  content: item.name,
                  padding: defaultCallout.padding,
                  color: defaultCallout.color,
                  bgColor: defaultCallout.bgColor,
                  textAlign: defaultCallout.textAlign,
                  //display: defaultCallout.display,
                  borderRadius: defaultCallout.borderRadius,
              }
          });

          newRegionPoints.push({ latitude: item.latitude, longitude: item.longitude });
      });

      this.mapCtx.includePoints({
          padding: [30, 30, 30, 30],
          points: newRegionPoints
      });

      page.setData({
          'map.markers': subMarkers,
      });
  },

    //统一导航函数
  nav: function (event) {
      var url = event.currentTarget.dataset.url;
      var lon = event.currentTarget.dataset.lon;
      var lat = event.currentTarget.dataset.lat;
      wx.navigateTo({
          url: url + "?lon=" + lon + "&lat=" + lat,
      });
  },


  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    page=this;
    page.mapCtx = wx.createMapContext('map');

      //获取容器高度
    var contentHeight = app.globalData.systemInfo.windowHeight-100/app.globalData.rpx-1;

    var controlSize = 20 * app.globalData.rpx;

      //设置地图控件
    var controls = [
      { id: "locateBtn", iconPath: '../../resources/images/map/locate.png', position: { left: controlSize / 3, top: 3.3 * controlSize, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomInBtn", iconPath: '../../resources/images/map/zoomIn.png', position: { left: controlSize / 3, top: controlSize / 3, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomOutBtn", iconPath: '../../resources/images/map/zoomOut.png', position: { left: controlSize / 3, top: controlSize / 3 + controlSize, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomAllBtn", iconPath: '../../resources/images/map/zoomAll.png', position: { left: controlSize / 3, top: 2.3 * controlSize, width: controlSize, height: controlSize }, clickable: true }];

    var spotId=options.spotId;
    if (spotId != undefined && spotId != null && spotId != "") {
      page.showInMap({
        currentTarget:{
          dataset:{
            id:spotId
          }
        }
      });
    }
    else page.initialMarkers();

    page.setData({
        contentHeight: contentHeight,
        'map.currentControls': controls,
        'map.allControls': controls
    });

    page.refreshStrategies();
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
    if (page.dataset.currentTab == "1") {
      page.refreshStrategies();
    }
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