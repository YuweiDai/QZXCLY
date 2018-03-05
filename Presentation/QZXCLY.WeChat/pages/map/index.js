// pages/map/index.js
var p=null;
var markers = [
  {
  iconPath: "/resources/others.png",
  id: 0,
  latitude: 29.1851002329, 
  longitude: 119.0333890915,
  width: 50,
  height: 50,
  callout: {
    content: "康养衢江 · 隐柿东坪",
    padding: 5,
    color: "#ffffff",
    bgColor: "#1296db",
    textAlign: "center",
    display: "ALWAYS",
    borderRadius: 5
  }
}, {
  iconPath: "/resources/others.png",
  id: 1,
  latitude: 29.1484507736,
  longitude: 118.7620890141,
  width: 50,
  height: 50,
  callout: {
    content: "寻觅乡愁 · 桃源七里",
    padding: 5,
    color: "#ffffff",
    bgColor: "#1296db",
    textAlign: "center",
    display: "ALWAYS",
    borderRadius: 5
  }
}, {
  iconPath: "/resources/others.png",
  id: 2,
  latitude: 28.8705329848,
  longitude: 118.8349914551,
  width: 50,
  height: 50,
  callout: {
    content: "航埠镇",
    padding: 5,
    color: "#ffffff",
    bgColor: "#1296db",
    textAlign: "center",
    display: "ALWAYS",
    borderRadius: 5
  }
},
{
  iconPath: "/resources/others.png",
  id: 3,
  latitude: 28.9240352884,
  longitude: 119.0578079224,
  width: 50,
  height: 50,
  callout: {
    content: "全旺镇",
    padding: 5,
    color: "#ffffff",
    bgColor: "#1296db",
    textAlign: "center",
    display: "ALWAYS",
    borderRadius: 5
  }
}, {
  iconPath: "/resources/others.png",
  id: 4,
  latitude: 28.5341631929,
  longitude: 118.5649681091,
  width: 50,
  height: 50,
  callout: {
    content: "江郎山",
    padding: 5,
    color: "#ffffff",
    bgColor: "#1296db",
    textAlign: "center",
    display: "ALWAYS",
    borderRadius: 5
  }
}
];
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    mapHeight: 400,
    contentHeight: 200,
    layout: 0,    //0表示最底层 1表示中间 2表示最顶层
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
        selected:true
      },
      {
        id: 1,
        title: "寻觅乡愁 · 桃源七里",
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/j1.jpg",
        latitude: 29.1484507736,
        longitude: 118.7620890141,
        selected: false
      },
      // {
      //   id: 2,
      //   title: "航埠镇",
      //   logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/j1.jpg",
      //   latitude: 29.1851002329,
      //   longitude: 119.0333890915,
      //   selected: false
      // },
      {
        id: 3,
        title: "全旺镇",
        logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/j2.jpg",
        latitude: 28.9240352884,
        longitude: 119.0578079224,
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

    lon: 118.8594317436,
    lat: 28.9702076731,
    level: 11,
    markers: markers,
    showSingelSpot: false,
    filterId:1,
    play_list: [
      {
        title: "千年古道",
        descriptions: "东坪千年古道位于衢州市峡川镇境内，距市区40公里。从东坪山脚下起，一条总长1500米，宽2米，共1144级的青石板古道蜿蜒盘曲伸向山顶，随着岁月的流逝，村民的脚步磨去了古道青石块的棱角而变得光滑。这就是相传具有1300多年历史的唐朝古道。",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "古道居",
        descriptions: "景点文字介绍",
        logo: "/resources/images/index/l2.png"
      },
      {
        title: "豆腐坊",
        descriptions: "景点文字介绍",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "华兰阁",
        descriptions: "景点文字介绍",
        logo: "/resources/images/index/l2.png"
      },
      {
        title: "东坪院",
        descriptions: "景点文字介绍",
        logo: "/resources/images/index/l2.png"
      },
      {
        title: "松青阁",
        descriptions: "景点文字介绍",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "冬雪苑",
        descriptions: "景点文字介绍",
        logo: "/resources/images/index/l2.png"
      }
    ],
    eat_list: [
      {
        title: "米兰农庄",
        descriptions: "衢州市峡川镇东坪村",
        tel: "13362121222",
        person: "罗老板",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "望乡楼",
        descriptions: "衢州市峡川镇东坪村",
        tel: "13812345678",
        person: "王老板",        
        logo: "/resources/images/index/l2.png"
      },
      {
        title: "老陈饭店",
        descriptions: "衢州市峡川镇东坪村",
        tel: "13362121222",
        person: "罗方剑",        
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "老赵饭店",
        descriptions: "衢州市七里乡黄土岭村7号",
        tel: "13362121222",
        person: "罗方剑",        
        logo: "/resources/images/index/l2.png"
      }
    ],
    live_list: [
      {
        title: "圃舍·源溪",
        descriptions: "柯城区九华乡新宅村大荫山下",
        tel: "13705706658",
        person: "罗方剑",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "庙源溪墅",
        descriptions: "九华乡茶铺村",
        tel: "18906708718",
        person: "罗方剑",
        logo: "/resources/images/index/l2.png"
      },
      {
        title: "关西山房",
        descriptions: "九华乡茶铺村",
        tel: "18906708718",
        person: "罗方剑",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "溢舍",
        descriptions: "九华乡茶铺村",
        tel: "18906708718",
        person: "罗方剑",
        logo: "/resources/images/index/l2.png"
      }
    ],
    traffic_list: [
      {
        title: "自驾路线（点击导航）",
        descriptions: "自驾游客，在杭金衢高速公路衢州西高速入口下，下高速后，在第一个丁字入口往左拐",
        logo: '/resources/images/index/icon_5.jpg'
      },
      {
        title: "公交502路",
        descriptions: "到衢州新火车站、衢州长途汽车站、衢州汽车南站后，乘坐7路公交或打的到衢州交警支队柯城大队站下，在街对面公交站牌处，乘坐502公交直达七里。502公交每隔40～50分钟一班。也可以乘坐其他公交线路或打的到斗潭站下，再向北步行220米左右，到达502公交乘坐处",
        logo: '/resources/images/index/icon_12.jpg'
      },

    ],    
    service_list: [
      {
        title: "集散中心",
        descriptions: "",
        logo: "/resources/images/map/service.png",
        lat: 29.1851985843, 
        lon: 119.0282231569
      },
      {
        title: "停车场",
        descriptions: "",
        logo: "/resources/images/map/park.png",
        lat: 29.1850627657, 
        lon: 119.0287113190
      },      
      {
        title: "公共厕所",
        descriptions: "",
        logo: "/resources/images/map/wc.png",
        lat: 29.1851939009, 
        lon: 119.0326166153
      },
      {
        title: "应急医疗点",
        descriptions: "",
        logo: "/resources/images/map/medical.png",
        lat: 29.1426786032, 
        lon: 118.9927589893,     
      },
    ],     
  },
  bindmarkertap: function (event) {
  
    console.log(event);
    var marker = markers[0];// markers[event.markerId];

    if(marker.id==undefined|| marker.id==null) return;

    this.setData({
      showSingelSpot: true,
      mapHeight: app.globalData.systemInfo.windowHeight - 276,
      contentHeight: 200,
    });

    var spotMarkers=[];
    spotMarkers.push(marker);

    this.data.service_list.forEach(function(item){
      var m = {
        iconPath: item.logo,
        latitude: item.lat,
        longitude: item.lon,
        width: 20,
        height: 20,
        callout: {
          content: item.title,
          padding: 5,
          color: "#ffffff",
          bgColor: "#1296db",
          textAlign: "center",
          borderRadius: 5
        }};

        spotMarkers.push(m);
    });

    this.setData({
      lon: marker.longitude,
      lat: marker.latitude,
      level: 18,
      markers: spotMarkers
    });
  },
  returnToAll:function(event){
    this.setData({
      lon: 118.8594317436,
      lat: 28.9702076731,
      level: 11,
      markers: markers,
      showSingelSpot: false,
      mapHeight: app.globalData.systemInfo.windowHeight,
      contentHeight: 0,      
    });
  },
  tapFilter: function (e) {

    this.setData({
      filterId: e.target.dataset.id,
    });
  },  
  moveTo:function(event)
  {
    var lon = event.currentTarget.dataset.lon;
    var lat = event.currentTarget.dataset.lat;

    if (lon == undefined || lat == null) return;

    this.setData({
      lon: lon,
      lat: lat,
      level:20
    });
  },
  navTo:function(event)
  {
    wx.redirectTo({
      url: 'nav',
    })
  },

  // 调整页面布局
  modifyLayout:function(event){
    var mapHeight = app.globalData.systemInfo.windowHeight * 0.7;
    var contentHeight = app.globalData.systemInfo.windowHeight * 0.3;

    var currentLayout=p.data.layout+1;
    if(currentLayout==3) currentLayout=0;
    switch (currentLayout)
    {
      case 1:
        mapHeight = app.globalData.systemInfo.windowHeight * 0.5;
        contentHeight = app.globalData.systemInfo.windowHeight * 0.5;
        break;
      case 2:
        mapHeight = app.globalData.systemInfo.windowHeight * 0.3;
        contentHeight = app.globalData.systemInfo.windowHeight * 0.7;
        break;                
    }

    this.setData({
      mapHeight: mapHeight,
      contentHeight: contentHeight,
      layout: currentLayout
    });  
  },

  spotsOnScroll:function(event)
  {
    // console.log(event.detail);
    var top=event.detail.scrollTop;
    var i=0;
    if(top>0)
      i = parseInt(top / 106)+1;

    // console.log(i);
    var allSpots = p.data.spots;
   allSpots[p.data.selectedSpot].selected=false;
    allSpots[i].selected=true;
    p.setData({
      selectedSpot:i,
      spots:allSpots
    });
  },
  
  /**
   * 生命周期函数--监听页面加载
   * 初始占20%
   * 地图占80%
   */
  onLoad: function (options) {
    if(p==null) p=this;

var markers=[];
    for(var index in p.data.spots)
    {
      markers.push({
        iconPath: p.data.spots[index].selected ?"/resources/images/map/marker.png":"/resources/images/map/spot.png",
        id: 4,
        latitude: p.data.spots[index].latitude,
        longitude: p.data.spots[index].longitude,
        width: 30,
        height: p.data.spots[index].selected ? 45: 30,
      });
    }

    this.setData({
      mapHeight: app.globalData.systemInfo.windowHeight * 0.7,
      contentHeight: app.globalData.systemInfo.windowHeight * 0.3,
      markers: markers
    });


    // wx.getLocation({
    //   type: 'wgs84',
    //   success: function (res) {
    //     var latitude = res.latitude;
    //     var longitude = res.longitude;
    //     var speed = res.speed;
    //     var accuracy = res.accuracy;

    //     // page.setData({ lon: longitude,lat:latitude });
    //   }
    // });

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