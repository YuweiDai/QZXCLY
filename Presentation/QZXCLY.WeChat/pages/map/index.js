// pages/map/index.js
var app = getApp();
var page = null;
var rpx=2;
var defaultCallout= {
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
    showModalStatus: false,    
    animationData:null,
    drawerMarker:null,
    markerSize:{
      width:44,
      height:64
    },
    selectedSpot:0,
    markers: [],
    polylines:[],
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
            page.setData({
              level: currentLevel,
            });
            console.log("now:" + page.data.level);
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
          padding: [30],
          points: page.data.currentRegionPoints
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
    var markerId = tappedMarkerId.split('_')[1];

    var drawerMarker = {
      logo: "",
      t: markerType,
      lon: 0,
      lat: 0,
      detailUrl: ""
    };
    if (page.data.currentSpot != null)
      drawerMarker.panoramaUrl = "https://www.luckyday.top/threejs?village=" + page.data.currentSpot.panorama + "&pid=1";

    switch(markerType)
    {
      case "spot":
        //景区点击事件
        if (page.data.showSingelSpot) return;
        var spotId = markerId;

        wx.showLoading({
          title: '数据加载中...',
        });

        var currentLon=0, currentLat=0;
        wx.getLocation({
          type:"gcj02",
          success: function(res) {
            currentLat = res.latitude
            currentLon = res.longitude
          },
          complete:function(){
            wx.request({
              url: app.globalData.apiUrl + 'villages/geo/' + spotId + '?lon='+currentLon + '&lat='+currentLat,
              success: function (response) {
                var spot = response.data;

                //更新空间位置
                var controls = page.data.allControls;
                controls[0].position.top += 200 / rpx;
                controls[1].position.top += 200 / rpx;
                controls[2].position.top += 200 / rpx;
                controls[3].position.top += 200 / rpx;

                wx.setNavigationBarTitle({
                  title: spot.name
                });

                page.setSubMarkers(spot, page.data.filterType);

                page.setData({
                  currentSpot: spot,
                  showSingelSpot: true,
                  currentControls: controls
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
        var play = null;
        page.data.currentSpot.plays.forEach(function (item) {
          if (item.id == markerId) {
            play = item;
            return false;
          }
        });
        if (play == null) return;
            drawerMarker.name = play.name;
            drawerMarker.logo = play.logo;
            drawerMarker.desc = play.description;
            drawerMarker.lon = play.longitude;
            drawerMarker.latitude = play.latitude;
            drawerMarker.audioUrl = play.audioUrl;
            if (play.panoramaId) drawerMarker.panoramaUrl += play.panoramaId;
            else drawerMarker.panoramaUrl=undefined;
            drawerMarker.detailUrl = "../index/spot_play?id="+play.id;
            
        break;
      case "eat":
        var eat = null;
        page.data.currentSpot.eats.forEach(function (item) {
          if (item.id == markerId) {
            eat = item;
            return false;
          }
        });
        if (eat == null) return;
          drawerMarker.name = eat.name;
          drawerMarker.logo = eat.logo;
          drawerMarker.lon = eat.longitude;
          drawerMarker.latitude = eat.latitude;
          if (eat.panoramaId) drawerMarker.panoramaUrl += eat.panoramaId;
          drawerMarker.phone = eat.tel;
          drawerMarker.price = eat.price;
          drawerMarker.level=eat.level;
          drawerMarker.detailUrl = "../index/spot_eat?id=" + eat.id;
          console.log(eat);
        break;
      case "live":
          var live = null;
          page.data.currentSpot.lives.forEach(function(item){
            if(item.id==markerId)
            {
              live=item;
              return false;
            }
          });
          if(live==null) return;          
          drawerMarker.name = live.name;
          drawerMarker.logo = live.logo;
          drawerMarker.lon = live.longitude;
          drawerMarker.latitude = live.latitude;
          drawerMarker.phone = live.tel;
          if (live.panoramaId) drawerMarker.panoramaUrl += live.panoramaId;
          drawerMarker.desc = live.description;
          drawerMarker.detailUrl = "../index/spot_live?id=" + live.id;
        break;                
    }    

    if (markerType == "play" || markerType == "eat" || markerType=="live")
    {
      if(drawerMarker.logo==""|| drawerMarker.logo==null)
      {
        var size=240/rpx;
        drawerMarker.logo ="http://www.atool.org/placeholder.png?size="+size+"x"+size+"&text="+drawerMarker.name+"&&bg=836&fg=fff";
      }
      console.log(drawerMarker);

      page.setData({
        drawerMarker: drawerMarker
      });
      page.powerDrawer();
    }

  },

  //返回所有景点
  returnToAll: function (event) {

    var controls = page.data.allControls;
    controls[0].position.top -= 200 / rpx;
    controls[1].position.top -= 200 / rpx;
    controls[2].position.top -= 200 / rpx;
    controls[3].position.top -= 200 / rpx;

    page.initialMarkers();

    page.setData({
      pls:[],
      currentSpot:null,
      currentControls: controls,
      showSingelSpot: false,
    });
  },

  //切换吃喝玩乐
  tapFilter: function (e) {

    var currentSpot = page.data.currentSpot;
    page.setSubMarkers(currentSpot, e.target.dataset.id);

    var pls=[];
    page.setData({
      filterType: e.target.dataset.id, polylines: pls
    });
  }, 

  //设置吃喝玩乐
  setSubMarkers:function(currentSpot,filterType){
    var subMarkers = [];
    var newRegionPoints = [];
    var targetItems=[];

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
      subMarkers.push({
        id:filterType+ "_" + item.id,
        iconPath: "../../resources/images/map/" + item.icon + ".png",
        latitude: item.latitude,
        longitude: item.longitude,
        height: page.data.markerSize.height / rpx,
        width: page.data.markerSize.width / rpx,
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
      markers:subMarkers,    
    });
  },

  //统一导航函数
  nav:function(event){
    var url = event.currentTarget.dataset.url;
    var lon = event.currentTarget.dataset.lon;
    var lat = event.currentTarget.dataset.lat;
    wx.navigateTo({
      url: url+"?lon="+lon+"&lat="+lat,
    });
  },

  powerDrawer: function (e) {
    var currentStatu = "close";
    if (e == undefined || e.currentTarget==undefined ) currentStatu = "open";
    else currentStatu = e.currentTarget.dataset.statu;
    this.util(currentStatu)
  },
  util: function (currentStatu) {
    // 弹窗初始化
    /* 动画部分 */
    // 第1步：创建动画实例   
    var animation = wx.createAnimation({
      duration: 200,  //动画时长  
      timingFunction: "linear", //线性  
      delay: 0  //0则不延迟  
    });

    // 第2步：这个动画实例赋给当前的动画实例  
    this.animation = animation;

    // 第3步：执行第一组动画  
    animation.opacity(0).rotateX(-100).step();

    // 第4步：导出动画对象赋给数据对象储存  
    page.setData({
      animationData: animation.export()
    })

    // 第5步：设置定时器到指定时候后，执行第二组动画  
    setTimeout(function () {
      // 执行第二组动画  
      animation.opacity(1).rotateX(0).step();
      // 给数据对象储存的第一组动画，更替为执行完第二组动画的动画对象  
      page.setData({
        animationData: animation
      })

      //关闭  
      if (currentStatu == "close") {
        page.setData(
          {
            showModalStatus: false
          }
        );
      }
    }.bind(this), 200)

    // 显示  
    if (currentStatu == "open") {
      page.setData(
        {
          showModalStatus: true
        }
      );
    }

  } ,

  //初始化获取点集合
  initialMarkers:function(){
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
            width: 40 * rpx,
            height: 40 * rpx,
            iconPath: "../../resources/images/map/" + spot.icon + ".png",
            latitude: spot.latitude,
            longitude: spot.longitude,
            callout: callout
          };
          spotMarkers.push(spotMarker);
        }
        page.mapCtx.includePoints({
          padding: [30],
          points: points
        });

        //设置数据
        page.setData({
          markers: spotMarkers,
          currentRegionPoints: points,
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


  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    page=this;
    page.mapCtx = wx.createMapContext('map');
    rpx =750/ app.globalData.systemInfo.screenWidth ;     

    var controlSize = 20 * rpx;

    //设置地图控件
    var controls = [
      { id: "locateBtn", iconPath: '../../resources/images/map/locate.png', position: { left: controlSize / 3, top: 3.3 * controlSize, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomInBtn", iconPath: '../../resources/images/map/zoomIn.png', position: { left: controlSize / 3, top: controlSize / 3, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomOutBtn", iconPath: '../../resources/images/map/zoomOut.png', position: { left: controlSize / 3, top: controlSize / 3 + controlSize, width: controlSize, height: controlSize }, clickable: true },
      { id: "zoomAllBtn", iconPath: '../../resources/images/map/zoomAll.png', position: { left: controlSize / 3, top: 2.3 * controlSize, width: controlSize, height: controlSize }, clickable: true }];

    page.setData({
      currentControls: controls,
      allControls: controls
    });      

    page.initialMarkers();


    console.log("onLoad");
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

    console.log("onReady");      
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

    console.log("onShow");

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {
    console.log("onHide");
  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {
    console.log("onUnload");
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