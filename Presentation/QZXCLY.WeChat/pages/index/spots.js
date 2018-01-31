// pages/index/spots.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    hidden: false,
    curNav: 1,
    curIndex: 0,
    cart: [],
    cartTotal: 0,
    navList: [
      {
        id: 1,
        name: '附近'
      },
      {
        id: 2,
        name: '柯城区'
      },
      {
        id: 3,
        name: '衢江区'
      },
      {
        id: 4,
        name: '江山市'
      },
      {
        id: 5,
        name: '开化县'
      },
      {
        id: 6,
        name: '常山县'
      },
      {
        id: 7,
        name: '龙游县'
      }      
    ],
    dishesList: [
      [
        {
          name: "康养衢江·隐柿东坪",
          img: "/resources/images/index/j3.jpg" ,
          num: 1,
          id: 2
        }
      ],
      [
        {
          name: "寻梦乡愁·桃源七里",
          img: "/resources/images/index/j1.jpg",
          num: 1,
          id: 1
        },        
        {
          name: "石梁镇",
          img: "/resources/images/index/j2.jpg",
          num: 2,
          id: 29
        }
      ],
      [
        {
          name: "康养衢江·隐柿东坪",
          img: "/resources/images/index/j3.jpg",
          num: 1,
          id: 2
        }
      ],
      [],
      [
        {
          name: "杨林镇",
          img: "/resources/images/index/j3.jpg",
          num: 1,
          id: 3
        }    
      ],
      [],
      [
        {
          name: "溪口镇",
          img: "/resources/images/index/j2.jpg",
          num: 1,
          id: 4
        }            
      ]

    ]
  },
  loadingChange() {
    setTimeout(() => {
      this.setData({
        hidden: true
      })
    }, 2000)
  },
  selectNav(event) {
    let id = event.target.dataset.id,
      index = parseInt(event.target.dataset.index);
    self = this;
    this.setData({
      curNav: id,
      curIndex: index
    })
  },
  navToSpot(event){
    wx.navigateTo({
      url: 'spot?id='+event.currentTarget.dataset.id,
    });

  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad() {
    this.loadingChange()
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
});