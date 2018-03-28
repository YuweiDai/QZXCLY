// pages/index/spot.js
var app = getApp();
var page=null;
var innerAudioContext = null;
Page({
  /**
   * 页面的初始数据
   */
  data: {
    dialog:{
      visible:false,
      title:"",
      content:""
    },
    spot:null,
    filterId:"0", 
    audioPlayer:{
      playing:false,
      current:""
    },
    windowHeight:0,
    rpx: 750 / 375
  },
  tapFilter: function (e) {    
    this.setData({
      filterId: e.target.dataset.id, 
    });
  },
  makePhoneCall:function(event){
    var phone = event.currentTarget.dataset.phone;
     wx.showModal({
      title: '提示',
      content: '是否拨打电话 ' + phone+' ?',
      success: function (res) {
        if (res.confirm) {
          wx.makePhoneCall({
            phoneNumber: phone
          }); 
        }
      }
    })
  },
  //跳至地图导航
  navToMap:function(event){
    var url = event.currentTarget.dataset.url;
    var lon = event.currentTarget.dataset.lon;
    var lat = event.currentTarget.dataset.lat;
    wx.navigateTo({
      url: url + "?lon=" + lon + "&lat=" + lat,
    });
  },
  //页面转跳
  navTo: function (event) {
    var url=event.currentTarget.dataset.url;
    wx.navigateTo({
      url: url,
    })
  },
  playAudio:function(event)
  {   
    console.log(event);
    var page=this;

    var audioSrc = event.currentTarget.dataset.audio;
    if(audioSrc!="")
    {
      var same = this.data.audioPlayer.current == audioSrc;

      if (this.data.audioPlayer.playing) {
        if(same)
          innerAudioContext.pause();
        else 
        {
          if (!same) {
            innerAudioContext.src = audioSrc;
          }
          innerAudioContext.play();

        }
      }
      else {
        if (!same)
        {
          innerAudioContext.src = audioSrc;
        }
        innerAudioContext.play();
       
      }
     
    }    
  },
  updateAudioState: function (playing, audioSrc){ 
    this.setData({
      audioPlayer: {
        playing: playing,
        current: audioSrc
      }
    });
  },
  showTourJpg:function(event){
    wx.previewImage({
      urls: [page.data.spot.routePicutre],
    })
  },
  showDialog:function(event){
    page.setData({
      'dialog.visible': true,
      'dialog.title': event.currentTarget.dataset.title,
      'dialog.content': event.currentTarget.dataset.content      
    });
  },
  closeDialog:function(){
    page.setData({
      'dialog.visible':false
    });
  },


  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    page=this;
    if(innerAudioContext==null)
      innerAudioContext = wx.createInnerAudioContext();

    innerAudioContext.onPlay(() => {
      console.log('开始播放');
      page.updateAudioState(true, innerAudioContext.src);
    });
    innerAudioContext.onStop(() => {
      console.log('onStop');
      page.updateAudioState(false,"");
    })
    innerAudioContext.onPause(() => {
      console.log('onPause');
      page.updateAudioState(false, "");
    });
    innerAudioContext.onEnded(() => {
      console.log('onEnded');
      page.updateAudioState(false, "");
    });      
    page.setData({
      windowHeight: app.globalData.systemInfo.windowHeight,
      rpx: 750 / app.globalData.systemInfo.screenWidth
    });

    var id = options.id;

    if (id != 1 && id != 2) 

    wx.showModal({
      title: '提示',
      content: '数据暂未入库',
      showCancel:false,
      success: function (res) {
        if (res.confirm) {
          wx.redirectTo({
            url: 'index'
          });
        }
      }
    });

    //获取数据
    wx.showLoading({
      title: '数据加载中...',
    });
    //获取景区点
    wx.request({
      url: app.globalData.apiUrl + 'Villages/'+id,
      success: function (response) {
        var spot=response.data;
        var size = 240 / page.data.rpx;

        spot.tags=spot.tags.split(';');

        spot.strategies.forEach(function(item){
          item.src = app.globalData.resourceUrl+"/strategies/"+item.src;
        });
        
          

        console.log(spot);

        page.setData({
          spot: spot
        });

        wx.setNavigationBarTitle({
          title: spot.name
        });
      },
      fail: function (response) {
        wx.showToast({
          title: '获取数据失败...',
        })
      },
      complete: function () {
        wx.hideLoading();
      }
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
    if(innerAudioContext!=null)
      innerAudioContext.stop();  
  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {
    if (innerAudioContext != null)
      innerAudioContext.stop(); 
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