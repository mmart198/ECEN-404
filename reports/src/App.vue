<template>
  <v-app id="inspire" style="background:#262626;">
    <v-app-bar
      app
      color="#800000"
      dark
    >
      <v-toolbar-title>Generated Report</v-toolbar-title>
    </v-app-bar>

    <v-content>
      <v-container
        class="fill-height"
        fluid
      >
      <v-row align="center" justify="center">
        <v-col cols="1" sm="6" md="8">
          <div class="report" v-if="results" style="text-align:center;">
            <p>Optical Fibre Diameter Analyser (OFDA 100)</p>
            <p style="margin-top:-10px;">TEST REPORT</p>
            <p style="margin-top:-10px;font-size:14px;">This Test Performed According to 1.W.T.O Method 47 and 57</p>
            <v-container>
               <v-row class="mb-2" align="center" justify="center">
                 <v-col>
                      <p>Customer: {{results[0]["customerName"]}}</p>
                 </v-col>
                 <v-col>
                      <p>Date: {{results[0]["date_Time"]}}</p>
                </v-col>
               </v-row>
            </v-container>
            <p>Animal and Sample Description</p>
            <p>Sample ID: {{results[0]["sampleID"]}}</p>
            <p></p>
            
            
            <!--
            <p>{{results[0]["sampleName"]}}</p>
            <p>{{results[0]["companyName"]}}</p>
            <p>{{results[0]["sampleLog"]}}</p>
            -->

            <!--<p>{{results[0]["totalFiberCount"]}}</p>-->
            <p style="padding-left:10px;"> Mean FIbre Diameter (um): {{results[0]["meanDiameter"]}}</p>
            <p style="padding-left:10px;">Standard Deviation (%): {{results[0]["std"]}}</p>
            <v-row style="padding-left:9px;">
              <p style="padding-left:9px;">Coefficient of Variation (%): {{results[0]["variationCoefficient"]}}</p>
              <p style="padding-left:9px;">{{results[0]["cem"]}}</p>
              <p style="padding-left:9px;">Comfort Factor (%): {{results[0]["comfortFactor"]}}</p>
            </v-row>
            <v-row>
              <!--
              <p style="padding-left:10px;">{{results[0]["minDiameter"]}}</p>
              <p style="padding-left:10px;">{{results[0]["maxDiameter"]}}</p>
              -->
            </v-row>
              <!--
              <p>{{results[0]["fibersHist"]}}</p>
              -->
            
            <v-row style="padding-left:9px;">
              <p style="padding-left:10px;">Mean Curvature (deg / mm): {{results[0]["meanCurvature"]}}</p>
              <p style="padding-left:10px;">SD Curvature (deg / mm{{results[0]["sdCurvature"]}}</p>
            </v-row>
           <v-row style="padding-left:9px;">
             <!--
              <p style="padding-left:10px;">{{results[0]["stapleLength"]}}</p>
              -->
            </v-row>
           <v-row style="padding-left:9px;">
             <!--
              <p style="padding-left:10px;">{{results[0]["minStaple"]}}</p>
              <p style="padding-left:10px;">{{results[0]["maxStaple"]}}</p>
              -->
            </v-row>


          </div>
            <v-text-field
              v-if="!displayResults"
              label="Solo"
              placeholder="Search by Sample ID"
              solo
              v-model="sampleID" 
            ></v-text-field>
            <v-row align="center" justify="center">
              <div>
                <apexchart v-if="displayResults" height = "500" width="1100" type="bar" :options="chartOptions" :series="series"></apexchart>
              </div>
            </v-row>
            <v-row align="center" justify="center" >
              <div class="my-2">
                <v-btn v-if="!displayResults" small color="#34ba5f" style="color:white;font-weight:bold;font-size:20px;" @click="searchSampleID(sampleID)">Go</v-btn>
                <v-btn v-else small color="#34ba5f" style="color:white;font-weight:bold;font-size:20px;" @click="reset()">Search Again</v-btn>
              </div>
            </v-row> 
            
        </v-col>
      </v-row>
      </v-container>
    </v-content>
    <v-footer
      color="#9e062a"
      app
    >
      <span class="white--text">ECEN 403</span>
    </v-footer>
  </v-app>
</template>

<script>
import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import VueApexCharts from 'vue-apexcharts'
Vue.component('apexchart', VueApexCharts)
 
Vue.use(VueAxios, axios)

  export default {
    props: {
      source: String,
    },

    data: () => ({
      sampleID: "",
      displayResults: false,
      results: null,
      series: [{
            name: 'Fiber Count',
            
            data: []
          }],
          chartOptions: {
            grid: {
                show: true,
                borderColor: '#474747',
                strokeDashArray: 0,
                position: 'back',
                xaxis: {
                    lines: {
                        show: false
                    }
                },
                yaxis: {
                    lines: {
                        show: true
                    }
                },
                row: {
                    colors: undefined,
                    opacity: 0.1
                },
                column: {
                    colors: undefined,
                    opacity: 0.1
                },
                padding: {
                    top: 0,
                    right: 0,
                    bottom: 0,
                    left: 0
                },
            },
            colors:['#fff'],
            chart: {
              //background: '#fff',
              foreColor:'#fff',
              height: 500,
              type: 'bar',
            },
            plotOptions: {
              bar: {
                dataLabels: {
                  position: 'top', // top, center, bottom
                },
              }
            },
            dataLabels: {
              enabled: true,
              formatter: function () {
                return "";
              },
              offsetY: -10,
              style: {
                fontSize: '12px',
                colors: ["#304758"]
              }
            },
            
            xaxis: {
              categories:Array.from(Array(61).keys()),
              position: 'bottom',
              title: {
                style: {
                color: '#fff'
                },
                text: 'Fiber Diameter (um)',
                offsetY: 10
              },
              axisBorder: {
                show: false
              },
              axisTicks: {
                show: true,
              },
              crosshairs: {
                fill: {
                  type: 'gradient',
                  gradient: {
                    colorFrom: '#D8E3F0',
                    colorTo: '#BED1E6',
                    stops: [0, 100],
                    opacityFrom: 0.4,
                    opacityTo: 0.5,
                  }
                }
              },
              tooltip: {
                enabled: true,
              }
            },
            yaxis: {
              title: {
                style: {
                color: '#fff'
                },
                text: 'Fiber Count',
                offsetX: 10
              },
              min: 0,
              max: 60,
              
              axisBorder: {
                show: true
              },
              axisTicks: {
                show: true
              },
              labels: {
                show: true,
                step:5,
                formatter: function (val) {
                  return val;
                }
              }

            
            },
            title: {
              text: 'Fiber Diameter (microns)',
              floating: false,
              offsetY: 10,
              align: 'center',
              style: {
                color: '#fff'
              }
            }
          },
    }),
    watch:{

    },
    methods:{
      reset(){
        this.sampleID = "";
        this.displayResults = false;
        this.results = null;
      },
      searchSampleID(ID){
        Vue.axios.get(`https://localhost:44352/api/search?sampleID=${ID}`,{
        crossdomain:true
  }).then((response) => {
          this.displayResults = true;
          console.log(response.data);
          this.results = response.data;
          this.series[0].data=this.getDataArr(response.data[0]["minDiameter"],response.data[0]["maxDiameter"],response.data[0]["fibersHist"]);
          console.log(this.series[0]);
        })
      },
      getDataArr(min,max,dataSt){
        var dataStr = dataSt.replace(/[[]']+/g,'')
        var vals = dataStr.split(',');0
        var i = 0;
        var retArr = [];
        var j = 0;
        for(i = 1 ; i <= 60 ; ++i){
            if(i >= min && i <= max){
                retArr.push(vals[j]);
                ++j;
            }
            else{
                retArr.push(0);
            }
        }
        return retArr;
      }
    }
  }
</script>
<style scoped>
.report{
  color: rgb(255, 255, 255);
  font-size: 20px;
}
</style>