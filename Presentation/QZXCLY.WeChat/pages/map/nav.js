// pages/map/nav.js
var amapFile = require('../../libs/amap-wx.js');//如：..­/..­/libs/amap-wx.js
var myAmapFun=null;
var p=null;

Page({

  /**
   * 页面的初始数据
   */
  data: {
    targetLon:0,
    targetLat:0,
    markers: [],
    distance: '',
    cost: '',
    polyline: []
  },

  //验证经纬度合法性
  validateLonAndLat:function(lon,lat)
  {
    if (lon == undefined || lon == null) return false;
    if (lat == undefined || lat == null) return false;

    if(isNaN(lon) || isNaN(lat)) return false;

    if (lon < -180 || lon > 180) return false;
    if (lat < -90 || lat > 90) return false;
  },

  goDetail: function () {
    console.log(p.data.targetLat);
    wx.openLocation({
      latitude: p.data.targetLat,
      longitude: p.data.targetLon
    });
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.mapCtx = wx.createMapContext('navi_map');

    if (p == null) p = this;
    var targetLon = parseFloat(options.lon);
    var targetLat = parseFloat(options.lat);
    if (p.validateLonAndLat(targetLon, targetLat)) {
      wx.showModal({
        title: '无法计算路线',
        content: '传入的经纬度数据错误',
        showCancel: false,
        success: function (res) {
          if (res.confirm) {
            wx.navigateBack();
          };
        }
      });
    }
    p.setData({
      targetLon: targetLon,
      targetLat: targetLat,
    });
        
    myAmapFun = new amapFile.AMapWX({ key: '95d2ad325d43f19cdd3fb2eac8cceadc' });

    wx.getLocation({
      success: function (res) {
        var latitude = res.latitude;
        var longitude = res.longitude;
        var speed = res.speed;
        var accuracy = res.accuracy;

        var startPoint = {
          iconPath: "../../resources/images/map/start.png",
          id: 0,
          latitude: latitude,
          longitude: longitude,
          width: 23,
          height: 33
        };
        var endPoint = {
          iconPath: "../../resources/images/map/end.png",
          id: 1,
          latitude: targetLat,
          longitude: targetLon,
          width: 24,
          height: 34
        };

        p.setData({ markers: [startPoint, endPoint] });

        p.mapCtx.includePoints({
          padding: [30],
          points: [{ latitude: latitude, longitude: longitude }, { latitude: p.data.targetLat, longitude: p.data.targetLon }]
        }); 

        myAmapFun.getDrivingRoute({
          origin: longitude + ',' + latitude,
          destination: targetLon + ',' + targetLat,
          success: function (data) {
            var points = [];
            if (data.paths && data.paths[0] && data.paths[0].steps) {
              var steps = data.paths[0].steps;
              for (var i = 0; i < steps.length; i++) {
                var poLen = steps[i].polyline.split(';');
                for (var j = 0; j < poLen.length; j++) {
                  points.push({
                    longitude: parseFloat(poLen[j].split(',')[0]),
                    latitude: parseFloat(poLen[j].split(',')[1])
                  })
                }
              }
            }
            p.setData({
              polyline: [{
                points: points,
                color: "#0091ff",
                width: 6
              }]
            });
            if (data.paths[0] && data.paths[0].distance) {
              p.setData({
                distance: data.paths[0].distance + '米'
              });
            }
            if (data.taxi_cost) {
              p.setData({
                cost: '打车约' + parseInt(data.taxi_cost) + '元'
              });
            }

          },
          fail: function (info) {

          }
        })
      },
    })    
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