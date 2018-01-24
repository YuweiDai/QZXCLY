// pages/map/index.js
var markers = [{
  iconPath: "/resources/others.png",
  id: 0,
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
  id: 1,
  latitude: 29.0147457221,
  longitude: 118.7917327881,
  width: 50,
  height: 50,
  callout: {
    content: "廿里镇",
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
    mapHeight:600,
    detailHeight: 0,    
    lon: 118.8594317436,
    lat: 28.9702076731,
    level: 11,
    markers: markers,
    showSingelSpot: false,
    filterId:1,
    play_list: [
      {
        title: "浙江省农家乐示范村——黄土岭",
        descriptions: "是典型的浙西山里人家民居村落，以村入口竹牌门楼始，1公里的谷地山间林中，点点民居，散落其中，廊桥横跨香溪，恰似一幅江南山水丹青画卷。由于山高谷深、空气凉爽，素有“小东北”之称。境内有山外山庄（黄土岭自然村）、涧石听泉、香溪观瀑、飞来奇石、际头峡谷、狮头瀑布、树王参天（浙江杉树王）、蝶鸟飞涧等自然景观。山清水秀，民风纯补，是一个休闲、避暑、度假的理想场所。",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "国家AA级旅游景区——杨坞村",
        descriptions: "杨坞村，旅游资源丰富，有民居建筑群、花岩林业观光园、三叠龙潭、杨花瀑布、红军墓、阳花尖等自然和人文景点。游览途中可一路观赏毛竹林、板栗林、柑橘林、红军墓、红豆杉、三叠龙潭和杨花瀑布等景点，感受自然的山野风光、绵绵不断地经济林景观和清澈透凉的溪流飞瀑。登上海拔1110米的阳花尖，峰峦奇秀、百里田畴和村落历历在目。返回后，到清风厅小憩，赏乡村风貌、品农家茶点、感受如诗山里人家。",
        logo: "/resources/images/index/l2.png"
      },
      {
        title: "深山状元村——均良",
        descriptions: "“笔架峰下诗书读；均良福地文运昌”便是均良村（文昌福地）的最佳诠释。自1970年来全村共培养出大中专生、本科生、研究生、和博士47名，其中博士后4名，硕士生6名,全村只有350人，占全村总人口数的13%左右。可谓是人才辈出，文运昌盛。中国传统的“耕读文化”在这个小山村里被演绎成一段“知识改变命运”的佳话。因此，当地人们称均良村为“状元村”。",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "高山蔬菜村——上村",
        descriptions: "南北延伸约数里的七里尖高山蔬菜基地，位于海拔800～1200m的上村，是省级无公害蔬菜基地，省级特色农业精品园区。高海拔、低气温、高含氧的特殊地理条件，使得盛产于此的茄子、四季豆、豌豆、西红柿、黄瓜，富含维生素、矿物质、蛋白质、碳水化合物。",
        logo: "/resources/images/index/l2.png"
      }
    ],

    eat_list: [
      {
        title: "桃源七里之山外山庄",
        descriptions: "衢州市七里乡黄土岭村7号",
        tel: "13362121222",
        person: "罗方剑",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "老张饭店",
        descriptions: "衢州市七里乡黄土岭村7号",
        logo: "/resources/images/index/l2.png"
      },
      {
        title: "老陈饭店",
        descriptions: "衢州市七里乡黄土岭村7号",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "老赵饭店",
        descriptions: "衢州市七里乡黄土岭村7号",
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
        lat: 29.1470874087, 
        lon: 118.7622070313 
      },
      {
        title: "售票大厅",
        descriptions: "",
        logo: "/resources/images/map/ticket.png",
        lat: 29.1471811113, 
        lon: 118.7629473209    
      },
      {
        title: "停车场",
        descriptions: "",
        logo: "/resources/images/map/park.png",
        lat: 29.1466516903,
        lon: 118.7611609697    
      },      
      {
        title: "公共厕所",
        descriptions: "",
        logo: "/resources/images/map/wc.png",
        lat: 29.1478323423, 
        lon: 118.7617832422
      },
      {
        title: "应急医疗点",
        descriptions: "",
        logo: "/resources/images/map/medical.png",
        lat: 29.1465720427,
        lon: 118.7624323368        
      },
    ],     
  },
  bindmarkertap: function (event) {
  
    console.log(event);
    var marker = markers[0];// markers[event.markerId];

if(marker.id==undefined|| marker.id==null) return;

    this.setData({
      showSingelSpot: true,
      mapHeight: app.globalData.deviceSize.height - 276,
      detailHeight: 200,
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
      mapHeight: app.globalData.deviceSize.height,
      detailHeight: 0,      
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
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var page = this;

    wx.getLocation({
      type: 'wgs84',
      success: function (res) {
        var latitude = res.latitude
        var longitude = res.longitude
        var speed = res.speed
        var accuracy = res.accuracy

        // page.setData({ lon: longitude,lat:latitude });
      }
    });

    if (app.globalData.locationDetect)
    {
      page.bindmarkertap({ markerId:1});
      app.globalData.locationDetect=false;
    }
    else
    {
      this.setData({
        mapHeight: app.globalData.deviceSize.height,
        detailHeight: 0,
      });

      console.log(this.data.mapHeight);
    }

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