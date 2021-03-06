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
var innerAudioContext = null;
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
          name: ''
        },
        {
          id: 1,
          img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b2.gif',
          url: '',
          name: ''
        },
        {
          id: 2,
          img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b3.gif',
          url: '',
          name: ''
        }
      ],
    },
    map:{
      showModalStatus: false,
      animationData: null,
      drawerMarker: null,
      markerSize: {
        width:44,
        height: 66
      },
      spotSize: {
        width: 20,
        height: 20
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
    strategiesPage:{
      size:5,
      index:-1
    },
    //景区详情
    detail:{
      dialog: {
        visible: false,
        title: "",
        content: ""
      },
      spot: null,
      filterId: "0",
      audioPlayer: {
        playing: false,
        current: ""
      },
      urls: [],
      strategies:[]
    },
    currentTab:"0", // -1 0 1  
    contentHeight:0,
    rpx:2
  },

  //切换显示
  setTab:function(event){
    var target=event.currentTarget.dataset.id;

    switch(target)
    {
      case "0":
        //切换视野
        this.mapCtx.includePoints({
          padding: [50,50,50,50],
          points: page.data.map.currentRegionPoints
        });
        break;
      case "1":
        if (page.data.map.currentSpot == null)
          page.refreshStrategies();
        else page.getStrategiesByVillage();
        break;
      case "2":
        var currentSpot=page.data.detail.spot;
        if(!(currentSpot!=null && currentSpot.id==page.data.map.currentSpot.id))
          page.getSpotDetail(page.data.map.currentSpot.id);
        break;
    }



    page.setData({
      currentTab:target
    });
  },

  //获取景区详情
  getSpotDetail:function(id){
    //获取数据
    wx.showLoading({
      title: '数据加载中...',
    });
    var size = 240 / page.data.rpx;
    var requestPromisified = util.wxPromisify(wx.request);

    requestPromisified({
      url: app.globalData.apiUrl + 'Villages/' + id,
    }).then(function (response) {
      var spot = response.data;

      spot.tags = spot.tags.split(';');

      spot.strategies.forEach(function (item) {
        item.src = app.globalData.resourceUrl + "strategies/" + item.src;
        item.img = item.img.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
      });

      spot.villagePictures.forEach(function (item) {
        item.src = item.src.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
      });

      //设置视频封面
      spot.videoLogo = spot.videoUrl.replace("mp4", "jpg");

      spot.logo = spot.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
      spot.routePicutre = spot.routePicutre.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);     
      console.log(spot);

      page.setData({
        'detail.spot': spot
      });

      wx.request({
        url: app.globalData.apiUrl + 'Villages/' + id + "/EatList",
        success: function (response) {
          var eats = response.data;
          eats.forEach(function (item) {
            if (item.logo == "" || item.logo == null)
              item.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + item.name + "&&bg=836&fg=fff";
            else
              item.logo = item.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
          });

          page.setData({
            'detail.spot.eats': eats
          });
        },
        fail: function (response) {

        },
        complete: function () { }

      });

      wx.request({
        url: app.globalData.apiUrl + 'Villages/' + id + "/PlayList",
        success: function (response) {
          var plays = response.data;
          plays.forEach(function (item) {
            if (item.logo == "" || item.logo == null)
              item.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + item.name + "&&bg=836&fg=fff";
            else
              item.logo = item.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);

            if (item.audioUrl != "" && item.audioUrl != null)
              item.audioUrl = item.audioUrl.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
          });
          page.setData({
            'detail.spot.plays': plays
          });
        },
        fail: function (response) {

        },
        complete: function () { }

      });

      wx.request({
        url: app.globalData.apiUrl + 'Villages/' + id + "/LiveList",
        success: function (response) {
          var lives = response.data;

          lives.forEach(function (item) {
            if (item.logo == "" || item.logo == null)
              item.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + item.name + "&&bg=836&fg=fff";
            else
              item.logo = item.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
          });
          page.setData({
            'detail.spot.lives': lives
          });
        },
        fail: function (response) {

        },
        complete: function () { }

      });
    }).then(function(){
    })
    .catch(function(){
      console.log("error in get spot detail");
    })
    .finally(function () {
      wx.hideLoading();
      console.log(page.data.detail);
    });     
  },

    //刷新获取攻略
  refreshStrategies:function(reset)
  {
    var requestPromisified = util.wxPromisify(wx.request);
    var pageIndex = page.data.strategiesPage.index;
    pageIndex++;
    if (reset) pageIndex = 0;

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
        'strategiesPage.index': pageIndex
      });
    }).catch(function (response) {
      console.log(response);
      wx.showToast({
        title: response//'获取数据失败...',
      })
    });    
  },

  //获取景区相关的攻略
  getStrategiesByVillage:function()
  {
    if (page.data.map.currentSpot == null || page.data.map.currentSpot == undefined) return;
    var spotId = page.data.map.currentSpot.id;
    if (page.data.detail.strategies == null || page.data.detail.strategies.length != 0) {
      page.setData({
        'strategies': page.data.detail.strategies
      });
    }
    else
    {
      var requestPromisified = util.wxPromisify(wx.request);

      requestPromisified({
        url: app.globalData.apiUrl + 'Strategy/village/' + spotId,
      }).then(function (response) {
        var strategies = response.data;

        strategies.forEach(function (item) {
          item.img = item.img.replace(app.globalData.apiUrl, app.globalData.picturesUrl);
          item.src = app.globalData.resourceUrl + "strategies/" + item.src;
        });
        page.setData({
          'strategies': strategies,
          'detail.strategies': strategies
        });
      }).catch(function (response) {
        console.log(response);
        wx.showToast({
          title: response.errMsg//'获取数据失败...',
        })
      });  
    }

  
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
    var sucess = function (response) {
      if (response.statusCode != 200) return;
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
          width: page.data.map.spotSize.width * app.globalData.rpx,
          height: page.data.map.spotSize.width * app.globalData.rpx,
          iconPath: "../../resources/images/map/" + spot.icon + ".png",
          latitude: spot.latitude,
          longitude: spot.longitude,
          callout: callout
        };
        console.log(spotMarker);
        spotMarkers.push(spotMarker);
      }
      page.mapCtx.includePoints({
        padding: [50, 50, 50, 50],
        points: points
      });

      //设置数据
      page.setData({
        'map.markers': spotMarkers,
        'map.currentRegionPoints': points,
      });
    }

      var fail = function (response) {
        wx.showToast({
          title: '加载地图点位错误',
          icon: "none"
        });
      };
      var complete = function () {
        wx.hideLoading();
      };
    app.request(app.globalData.apiUrl + 'Villages/geo','get',null,sucess,fail,complete);
 
  },

    //控件事件实现
  controltap: function (event) {
      switch (event.controlId) {         
        case "locateBtn":
          this.mapCtx.moveToLocation();
          break;
        case "nearByBtn":
          var controlls = page.data.map.allControls;
          if (page.data.map.currentControls.length == page.data.map.allControls.length)
          {                
            controlls[1].iconPath ="../../resources/images/map/nearBy.png";
            page.setData({
              'map.currentControls': [controlls[0], controlls[1], controlls[2], controlls[8]]
            });
          }
          else
          {
            controlls[1].iconPath = "../../resources/images/map/nearBy_s.png";
              page.setData({
                  'map.currentControls': controlls
              });
          }
          break;
        case "vrBtn":
          wx.navigateTo({
            url: 'webview?src=https://www.luckyday.top/' + page.data.map.currentSpot.panorama+'vtour',
          });
          break;
        case "labelBtn":

          var markers=page.data.map.markers;
          var off=false;

          markers.forEach(function(item){
            if (item.callout.display == "ALWAYS") { item.callout.display = "BYCLICK"; off = true; }
            else item.callout.display = "ALWAYS";
          });

          var controlls = page.data.map.allControls;
          controlls[8].iconPath = "../../resources/images/map/switch" + (!off ? "_s" : "") + ".png";
    

          page.setData({
            'map.markers':markers,
            'map.currentControls': controlls           
          });
          break;
        case "spotBtn":
        case "foodBtn":
        case "hotelBtn":
        case "parkBtn":
        case "washroomBtn":
          var targetFilter = "", orginFilter = page.data.map.filterType;
          var targetControlIndex = 0, orginControlIndex = 0;

          if (event.controlId == "spotBtn") {
            targetFilter = "play";
            targetControlIndex = 3;
          }
          else if (event.controlId == "foodBtn") {
            targetFilter = "eat";
            targetControlIndex = 4;
          } else if (event.controlId == "hotelBtn") {
            targetFilter = "live";
            targetControlIndex = 5;
          }
          else if (event.controlId == "parkBtn") {
            targetFilter = "park";
            targetControlIndex = 6;
          }
          else if (event.controlId == "washroomBtn") {
            targetFilter = "wash";
            targetControlIndex = 7;
          }

          orginControlIndex = page.data.map.filterType == "play" ? 3 : (page.data.map.filterType == "eat" ? 4 : (page.data.map.filterType == "live" ? 5 : (page.data.map.filterType == "park" ? 6 : 7)));


          if (targetFilter==orginFilter) return;
          var controlls = page.data.map.allControls;
          controlls[targetControlIndex].iconPath = controlls[targetControlIndex].iconPath.replace(".png", "_s.png");
          controlls[orginControlIndex].iconPath = controlls[orginControlIndex].iconPath.replace("_s.png", ".png");
          page.tapFilter({ target: { dataset: { id: targetFilter } } });
          page.setData({
            'map.currentControls': controlls
          });      
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

          wx.request({
              url: app.globalData.apiUrl + 'villages/geo/' + spotId ,
              success: function (response) {
                  var spot = response.data;
                  spot.logo = spot.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);

                  if (spot.level > 0) {
                      var index = 1;
                      spot.levelTag = "·";
                      for (var index = 1; index <= spot.level; index++) {
                          spot.levelTag += "A";
                      }
                      spot.levelTag += "级景区";
                  }

                  //更新空间位置
                  var allControls = page.data.map.allControls;
                  allControls[1].iconPath = "../../resources/images/map/nearBy_s.png";
                  wx.setNavigationBarTitle({
                      title: spot.name
                  });

                  page.setSubMarkers(spot, page.data.map.filterType);

                  page.setData({
                      'map.currentSpot': spot,
                      'map.showSingelSpot': true,
                      'map.currentControls': allControls
                  });
              },
              fail: function (response) {

              },
              complete: function () {
                  wx.hideLoading();
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
        case "park":
        case "wash":
          wx.showActionSheet({
            itemList: ['导航', '查看详情'],
            success: function (res) {
              switch(res.tapIndex)
              {
                  case 0:
                      console.log(event);
                      page.viewPath({ currentTarget: { dataset: { lon: event.longitude, lat: event.latitude } } });
                  break;
                case 1:
                    wx.navigateTo({
                        url: 'spot_service?id=' + markerId,
                    });
                  break;
              }
            },
            fail: function (res) {
              console.log(res.errMsg)
            }
          });        
          break;
      }

  },
    //返回所有景点
  returnToAll: function (event) {
      var controls = page.data.map.allControls;
      page.initialMarkers();

      page.setData({
          'map.currentSpot': null,
          'map.currentControls': [controls[0]],
          'map.showSingelSpot': false,
      });
  },
  //查看交通路线
  viewPath:function(event)
  {
    var url = '../map/nav';
    var lon = event.currentTarget.dataset.lon;
    var lat = event.currentTarget.dataset.lat;
    wx.navigateTo({
      url: url + "?lon=" + lon + "&lat=" + lat,
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
        case 'wash':
          currentSpot.services.forEach(function (item) {
            if (item.serviceType == 0) targetItems.push(item);
          });
          break;
        case 'park':
          currentSpot.services.forEach(function (item) {
            if (item.serviceType == 1) targetItems.push(item);
          });
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

        if (filterType == "play") {
            width = page.data.map.spotSize.width * app.globalData.rpx;
            height = page.data.map.spotSize.width * app.globalData.rpx;
        }

          subMarkers.push({
              id: filterType + "_" + item.id,
              iconPath: "../../resources/images/map/" + item.icon + ".png",
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
          points: newRegionPoints,
      });

      page.setData({
        'map.markers': subMarkers,
        'map.currentRegionPoints': newRegionPoints
      });
  },

  // 景区详情相关事件 开始
  detailTapFilter: function (e) {
    this.setData({
      'detail.filterId': e.target.dataset.id,
    });
  },
  playAudio: function (event) {
    var audioSrc = event.currentTarget.dataset.audio;
    if (audioSrc != "") {
      var same = page.data.detail.audioPlayer.current == audioSrc;

      if (page.data.detail.audioPlayer.playing) {
        if (same)
          innerAudioContext.pause();
        else {
          if (!same) {
            innerAudioContext.src = audioSrc;
          }
          innerAudioContext.play();

        }
      }
      else {
        if (!same) {
          innerAudioContext.src = audioSrc;
        }
        innerAudioContext.play();

      }

    }
  },
  updateAudioState: function (playing, audioSrc) {
    this.setData({
      'detail.audioPlayer.playing': playing,
      'detail.audioPlayer.current': audioSrc
    });
  },
  showTourJpg: function (event) {
    wx.previewImage({
      urls: [page.data.detail.spot.routePicutre],
    })
  },
  showDialog: function (event) {
    page.setData({
      'detail.dialog.visible': true,
      'detail.dialog.title': event.currentTarget.dataset.title,
      'detail.dialog.content': event.currentTarget.dataset.content
    });
  },
  closeDialog: function () {
    page.setData({
      'detail.dialog.visible': false
    });
  },

  showImgs: function () {
    if (page.data.detail.spot != null) {
      if (page.data.detail.urls.length == 0) {
        var requestPromisified = util.wxPromisify(wx.request)

        requestPromisified({
          url: app.globalData.apiUrl + "Villages/" + page.data.detail.spot.id + "/pictures",
        }).then(function (res) {
          var urls = [];
          res.data.forEach(function (item) {
            item.src = item.src.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
            urls.push(item.src);
          });

          page.setData({ 'detail.urls': urls });
          page.previewImgs();
        }).catch(function () {
        })
      }
      else page.previewImgs();

    };
  },

  previewImgs: function () {
    wx.previewImage({
      urls: page.data.detail.urls,
    });
  },

  // 景区详情相关事件 结束

  //通用方法
  makePhoneCall: function (event) {
    var phone = event.currentTarget.dataset.phone;
    wx.showModal({
      title: '提示',
      content: '是否拨打电话 ' + phone + ' ?',
      success: function (res) {
        if (res.confirm) {
          wx.makePhoneCall({
            phoneNumber: phone
          });
        }
      }
    })
  },
  //页面转跳
  navTo: function (event) {
    var url = event.currentTarget.dataset.url;
    wx.navigateTo({
      url: url,
    })
  },

  playVideo:function(event)
  {
    var requestPromisified = util.wxPromisify(wx.getNetworkType);
    var contiuePlay=true;
    var videoSize=parseInt(event.currentTarget.dataset.videoSize/1024);
    requestPromisified().then(function (res) {
      // 返回网络类型, 有效值：
      // wifi/2g/3g/4g/unknown(Android下不常见的网络类型)/none(无网络)
      var networkType = res.networkType;
      if (networkType == '2g' || networkType == '3g' || networkType == '4g' )
      {
        wx.showModal({
          title: '提示',
          content: '当前处于移动网络，视频总流量约' + videoSize + 'M，是否继续观看视频？',
          success: function (res) {
            if (res.cancel) contiuePlay = false;

            if (contiuePlay) {
              page.videoCtx.requestFullScreen();
              page.videoCtx.play();
            }
          }
        })
      }
    })
    .catch(function () {
      console.log("error in play video");
    });
  },

  closeVideo: function (event) {
    page.videoCtx.exitFullScreen();
  },

  //通用方法 结束

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    page=this;
    page.mapCtx = wx.createMapContext('map');
    page.videoCtx = wx.createVideoContext('videoPlayer', this);

    if (innerAudioContext == null)
      innerAudioContext = wx.createInnerAudioContext();

    innerAudioContext.onPlay(() => {
      console.log('开始播放');
      page.updateAudioState(true, innerAudioContext.src);
    });
    innerAudioContext.onStop(() => {
      console.log('onStop');
      page.updateAudioState(false, "");
    })
    innerAudioContext.onPause(() => {
      console.log('onPause');
      page.updateAudioState(false, "");
    });
    innerAudioContext.onEnded(() => {
      console.log('onEnded');
      page.updateAudioState(false, "");
    });

      //获取容器高度
    var contentHeight = app.globalData.systemInfo.windowHeight-100/app.globalData.rpx-1;

    var controlSize = 20 * app.globalData.rpx;

    var windowHeight = app.globalData.systemInfo.windowHeight;
    var windowWidth = app.globalData.systemInfo.windowWidth;

    var offsetTop=100 / app.globalData.rpx;
    console.log(windowHeight - 50 / app.globalData.rpx);

      //设置地图控件
    var controls = [
      { id: "locateBtn", iconPath: '../../resources/images/map/locate.png', position: { left: controlSize / 3, top: windowHeight - 250 / app.globalData.rpx, width: controlSize, height: controlSize }, clickable: true },
      { id: "nearByBtn", iconPath: '../../resources/images/map/nearBy.png', position: { left: controlSize / 3, top: controlSize * (1 / 3) + offsetTop, width: controlSize, height: controlSize }, clickable: true },
      { id: "vrBtn", iconPath: '../../resources/images/map/vr.png', position: { left: windowWidth - controlSize * (4 / 3), top: controlSize / 3 + offsetTop, width: controlSize, height: controlSize }, clickable: true },
      { id: "spotBtn", iconPath: '../../resources/images/map/spot_s.png', position: { left: controlSize / 3, top: controlSize * (5 / 3) + offsetTop, width: controlSize, height: controlSize }, clickable: true },
      { id: "foodBtn", iconPath: '../../resources/images/map/food.png', position: { left: controlSize / 3, top: controlSize * (8 / 3) + offsetTop, width: controlSize, height: controlSize }, clickable: true },
      { id: "hotelBtn", iconPath: '../../resources/images/map/hotel.png', position: { left: controlSize / 3, top: controlSize * (11 / 3) + offsetTop, width: controlSize, height: controlSize }, clickable: true },
      { id: "parkBtn", iconPath: '../../resources/images/map/park.png', position: { left: controlSize / 3, top: controlSize * (14 / 3) + offsetTop, width: controlSize, height: controlSize }, clickable: true },
      { id: "washroomBtn", iconPath: '../../resources/images/map/washroom.png', position: { left: controlSize / 3, top: controlSize * (17 / 3) + offsetTop, width: controlSize, height: controlSize }, clickable: true },
      { id: "labelBtn", iconPath: '../../resources/images/map/switch.png', position: { left: windowWidth - controlSize * (4 / 3), top: windowHeight - 250 / app.globalData.rpx, width: controlSize, height: controlSize }, clickable: true },
    ];

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
        rpx:app.globalData.rpx,
        contentHeight: contentHeight,
        'map.currentControls': [controls[0]],
        'map.allControls': controls
    });


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
    wx.showToast({
      title: 'onPullDownRefresh',
      icon: 'success',
      duration: 2000
    });
  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {
    // console.log("onPullDownRefresh");
    // if (page.dataset.currentTab == "1") {
    //   page.refreshStrategies();
    // }
    wx.showToast({
      title: 'onReachBottom',
      icon: 'success',
      duration: 2000
    })    
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {
  
  }
})