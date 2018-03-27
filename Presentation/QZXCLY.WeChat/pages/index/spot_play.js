// pages/index/spot_subitem.js
var page=null;
var innerAudioContext=null;
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    //id: 0,
    //title: "千年古道",
    //description: "东坪千年古道位于衢州市峡川镇境内，距市区40公里。从东坪山脚下起，一条总长1500米，宽2米，共1144级的青石板古道蜿蜒盘曲伸向山顶，随着岁月的流逝，村民的脚步磨去了古道青石块的棱角而变得光滑。这就是相传具有1300多年历史的唐朝古道。",
    //logo: "http://qzch.qz.gov.cn/qzxcly//resources/images/index/l1.jpg",
    //audio: "http://sc1.111ttt.cn/2017/1/11/11/304112004168.mp3",
    //panorama: "",
    //photos: [
    //  { title: "古道1", img: "http://img8.blog.eastmoney.com/zl/zltkg/201505/20150530075406190.jpg" },
    //  { title: "古道2", img: "http://s9.rr.itc.cn/r/wapChange/20169_12_15/a29q5o2502122756352.jpg" },
    //  { title: "古道3", img: "http://img2.niwota.com/album/images/2015-07-15/1436925113922-club.jpg" },
    //  { title: "古道4", img: "http://www.qlx123.com/files/2015-11/20151114161243130592.jpg" }  
    //],
    play:null,
    urls:'',
    playing:false
  },

  // 语音讲解
  playAudio: function (event) {
    var audioSrc = event.currentTarget.dataset.audio;
    if (audioSrc != "") {
      if (this.data.playing) {
        innerAudioContext.stop();
      }
      else {
        innerAudioContext.play();
      }
    }
  },

  // 切换至全景
  navToPanorama:function(event)
  {
    wx.navigateTo({
      url: 'webview?title=全景图&src=http://www.ipanocloud.com/tour/share/151029AEQ1O',
    });
  },

  // 地图导航
  navTo:function(event)
  {
    wx.navigateTo({
      url: '../map/nav',
    })
  },

  // 点击图片预览
  previewImage:function(event)
  {
    var url=event.currentTarget.dataset.url;
    wx.previewImage({
      current: url, // 当前显示图片的http链接
      urls:page.data.urls // 需要预览的图片http链接列表
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
      page=this;
    
      var id = options.id;

      if (id == "" || id == null) wx.navigateBack({});

      //获取数据
      wx.showLoading({
          title: '数据加载中...',
      });
      //获取景区点
      wx.request({
          url: app.globalData.apiUrl + 'Villages/Play/' + id,
          success: function (response) {
              var play = response.data;

              if (play.logo == "" || play.logo == null)
                  play.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + play.name + "&&bg=836&fg=fff";


              console.log(play);

              var urls = [];
              for (var index in play.playPictures) {
                urls.push(play.playPictures[index].img);
              }

              page.setData({
                  play: play,
                  urls:urls
              });

              wx.setNavigationBarTitle({
                  title: play.name
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

    if (innerAudioContext == null)
    {
      innerAudioContext = wx.createInnerAudioContext();
      innerAudioContext.src=page.data.audio;
    }

    innerAudioContext.onPlay(() => {
      console.log('开始播放');
      page.setData({playing: true});
    });
    innerAudioContext.onStop(() => {
      console.log('onStop');
      page.setData({ playing: false });
    })
    innerAudioContext.onEnded(() => {
      console.log('onEnded');
      page.setData({ playing: false });
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