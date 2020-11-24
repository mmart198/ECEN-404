<template>
  <div>
    <v-btn
      style="
        position: fixed;
        margin-top: 70px;
        margin-left: 15px;
        z-index: 999;
      "
      depressed
      color="primary"
      @click="generateReport"
    >
      Generate PDF
    </v-btn>
    <vue-html2pdf
      style="display: none"
      :show-layout="false"
      :float-layout="false"
      :enable-download="true"
      :preview-modal="false"
      :paginate-elements-by-height="2000"
      filename="Report"
      :pdf-quality="10"
      :manual-pagination="false"
      pdf-format="a4"
      pdf-orientation="portrait"
      pdf-content-width="1050px;"
      ref="html2Pdf"
    >
      <section slot="pdf-content">
        <v-content>
          <v-container
            class="fill-height"
            style="margin-top: -90px; color: black !important"
            fluid
          >
            <v-row align="center" justify="center">
              <v-col cols="1" sm="7" md="9">
                <div
                  class="report slide-in-bck-center"
                  v-if="results && results.length"
                  style="text-align: center; margin-left: 20px"
                >
                  <p
                    style="
                      margin-top: 40px;
                      font-size: 34px;
                      color: black !important;
                    "
                  >
                    Optical Fibre Diameter Analyser (OFDA 100)
                  </p>
                  <p
                    style="
                      margin-top: 10px;
                      font-size: 34px;
                      color: black !important;
                    "
                  >
                    Test Report
                  </p>
                  <p
                    style="
                      margin-top: 10px;
                      font-size: 14px;
                      color: black !important;
                      margin-bottom: 20px;
                    "
                  >
                    This Test Performed According to 1.W.T.O Method 47 and 57
                  </p>
                  <div style="display: flex; width: 100%">
                    <div
                      style="
                        width: 50%;
                        text-align: left;
                        color: black !important;
                        font-size: 17px;
                      "
                    >
                      <p>Customer: {{ results[0]["customerName"] }}</p>
                    </div>
                    <div
                      style="
                        width: 50%;
                        text-align: left;
                        color: black !important;
                        font-size: 17px;
                        margin-bottom: 10px;
                      "
                    >
                      <p>Date: {{ results[0]["date_Time"] }}</p>
                    </div>
                  </div>
                  <p
                    style="
                      font-weight: bold;
                      font-size: 28px;
                      color: black !important;
                      margin-top: 10px;
                      margin-bottom: 10px;
                    "
                  >
                    Animal and Sample Description
                  </p>
                  <div
                    style="
                      display: flex;
                      width: 100%;
                      color: black !important;
                      font-size: 17px;
                    "
                  >
                    <div style="width: 50%; text-align: left">
                      <div>
                        <p class="animal-stats" style="color: black !important">
                          Sample ID: {{ results[0]["sampleID"] }}
                        </p>
                      </div>
                      <div>
                        <p class="animal-stats" style="margin-bottom: 10px">
                          Breed:
                        </p>
                      </div>
                      <div>
                        <p class="animal-stats" style="margin-bottom: 10px">
                          Sex:
                        </p>
                      </div>
                      <div>
                        <p class="animal-stats" style="margin-bottom: 10px">
                          Color:
                        </p>
                      </div>
                    </div>
                    <div style="width: 50%; text-align: left">
                      <div>
                        <p class="animal-stats" style="margin-bottom: 10px">
                          Sample Location:
                        </p>
                      </div>
                      <div>
                        <p class="animal-stats">
                          Sample Date: {{ results[0]["date_Time"] }}
                        </p>
                      </div>
                    </div>
                  </div>
                  <p
                    style="
                      font-size: 28px;
                      font-weight: bold;
                      color: black !important;
                      margin-top: 10px;
                      margin-bottom: 10px;
                    "
                  >
                    Laboratory Data
                  </p>
                  <div style="display: flex; width: 100%">
                    <div
                      style="
                        width: 33%;
                        text-align: left;
                        color: black !important;
                        font-size: 17px;
                      "
                    >
                      <div>
                        <p class="animal-stats" style="margin-bottom: 10px">
                          Mean Fibre Diameter (um):
                          {{ results[0]["meanDiameter"] }}
                        </p>
                      </div>
                      <br />
                      <div>
                        <p class="animal-stats" style="margin-bottom: 10px">
                          Mean Curvature (deg/mm):
                          {{ results[0]["meanCurvature"] }}
                        </p>
                      </div>
                    </div>
                    <div
                      style="
                        width: 33%;
                        text-align: left;
                        color: black !important;
                        font-size: 17px;
                      "
                    >
                      <div>
                        <p class="animal-stats" style="margin-bottom: 10px">
                          Standard Deviation (%): {{ results[0]["std"] }}
                        </p>
                      </div>
                      <div>
                        <p
                          class="animal-stats"
                          style="margin-bottom: 10px; font-size: 17px"
                        >
                          Coefficient of Variation (%):
                          {{ results[0]["variationCoefficient"] }}
                        </p>
                      </div>
                      <div>
                        <p
                          class="animal-stats"
                          style="margin-bottom: 10px; font-size: 17px"
                        >
                          SD Curvature (deg/mm): {{ results[0]["sdCurvature"] }}
                        </p>
                      </div>
                    </div>
                    <div
                      style="
                        width: 33%;
                        text-align: left;
                        color: black !important;
                      "
                    >
                      <div>
                        <p
                          class="animal-stats"
                          style="margin-bottom: 10px; font-size: 17px"
                        >
                          Comfort Factor (%): {{ results[0]["comfortFactor"] }}
                        </p>
                      </div>
                      <div>
                        <p
                          class="animal-stats"
                          style="margin-bottom: 10px; font-size: 17px"
                        >
                          Spin Fineness (um):
                        </p>
                      </div>
                    </div>
                  </div>
                </div>
                <v-row
                  align="center"
                  justify="center"
                  v-if="results && results.length"
                >
                  <div>
                    <apexchart
                      v-if="displayResults"
                      style="padding-left: 40px"
                      height="500"
                      width="700"
                      type="bar"
                      :options="chartOptions2"
                      :series="series"
                    ></apexchart>
                  </div>
                </v-row>
              </v-col>
            </v-row>
          </v-container>
        </v-content>
      </section>
    </vue-html2pdf>
    <v-content>
      <v-container class="fill-height" style="margin-top: -20px" fluid>
        <v-row align="center" justify="center">
          <v-col cols="1" sm="7" md="9">
            <div
              class="report slide-in-bck-center"
              v-if="results && results.length"
              style="text-align: center"
            >
              <p style="font-size: 34px">
                Optical Fibre Diameter Analyser (OFDA 100)
              </p>
              <p style="margin-top: -10px; font-size: 34px">Test Report</p>
              <p style="margin-top: -10px; font-size: 14px">
                This Test Performed According to 1.W.T.O Method 47 and 57
              </p>
              <div style="display: flex; width: 100%">
                <div style="width: 50%; text-align: left; margin-left: 10px">
                  <p>Customer: {{ results[0]["customerName"] }}</p>
                </div>
                <div style="width: 50%; text-align: left">
                  <p>Date: {{ results[0]["date_Time"] }}</p>
                </div>
              </div>
              <p style="font-weight: bold; font-size: 28px">
                Animal and Sample Description
              </p>
              <div style="display: flex; width: 100%">
                <div style="width: 50%; text-align: left">
                  <div>
                    <p class="animal-stats">
                      Sample ID: {{ results[0]["sampleID"] }}
                    </p>
                  </div>
                  <div>
                    <p class="animal-stats">Breed:</p>
                  </div>
                  <div>
                    <p class="animal-stats">Sex:</p>
                  </div>
                  <div>
                    <p class="animal-stats">Color:</p>
                  </div>
                </div>
                <div style="width: 50%; text-align: left">
                  <div>
                    <p class="animal-stats">Sample Location:</p>
                  </div>
                  <div>
                    <p class="animal-stats">
                      Sample Date: {{ results[0]["date_Time"] }}
                    </p>
                  </div>
                </div>
              </div>
              <p style="font-size: 28px; font-weight: bold">Laboratory Data</p>
              <div style="display: flex; width: 100%">
                <div style="width: 33%; text-align: left">
                  <div>
                    <p class="animal-stats">
                      Mean Fibre Diameter (um):
                      {{ results[0]["meanDiameter"] }}
                    </p>
                  </div>
                  <br />
                  <div>
                    <p class="animal-stats">
                      Mean Curvature (deg/mm):
                      {{ results[0]["meanCurvature"] }}
                    </p>
                  </div>
                </div>
                <div style="width: 33%; text-align: left">
                  <div>
                    <p class="animal-stats">
                      Standard Deviation (%): {{ results[0]["std"] }}
                    </p>
                  </div>
                  <div>
                    <p class="animal-stats">
                      Coefficient of Variation (%):
                      {{ results[0]["variationCoefficient"] }}
                    </p>
                  </div>
                  <div>
                    <p class="animal-stats">
                      SD Curvature (deg/mm): {{ results[0]["sdCurvature"] }}
                    </p>
                  </div>
                </div>
                <div style="width: 33%; text-align: left">
                  <div>
                    <p class="animal-stats">
                      Comfort Factor (%): {{ results[0]["comfortFactor"] }}
                    </p>
                  </div>
                  <div>
                    <p class="animal-stats">Spin Fineness (um):</p>
                  </div>
                </div>
              </div>
            </div>
            <div
              class="slide-in-top"
              style="width: 100%; display: flex; justify-content: center"
            >
              <div v-if="isLoading" class="lds-ellipsis">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
              </div>
              <div
                v-if="!isLoading && displayResults && !results"
                style="text-align: center"
              >
                <p style="font-size: 24px; color: white">
                  Could not find any records for Sample ID {{ sampleID }}
                </p>
                <i
                  class="fa fa-search"
                  style="font-size: 30px; color: white"
                ></i>
              </div>
            </div>
            <v-row
              align="center"
              justify="center"
              v-if="results && results.length"
            >
              <div>
                <apexchart
                  v-if="displayResults"
                  height="500"
                  width="1100"
                  type="bar"
                  :options="chartOptions"
                  :series="series"
                ></apexchart>
              </div>
            </v-row>
          </v-col>
        </v-row>
      </v-container>
    </v-content>
    <link
      rel="stylesheet"
      href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"
    />
  </div>
</template>
<script>
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import VueApexCharts from "vue-apexcharts";
Vue.component("apexchart", VueApexCharts);

Vue.use(VueAxios, axios);

export default {
  props: {
    source: String,
  },

  data: () => ({
    sampleID: "",
    displayResults: false,
    isLoading: false,
    results: null,
    series: [
      {
        name: "Fiber Count",

        data: [],
      },
    ],
    chartOptions: {
      grid: {
        show: true,
        borderColor: "#474747",
        strokeDashArray: 0,
        position: "back",
        xaxis: {
          lines: {
            show: false,
          },
        },
        yaxis: {
          lines: {
            show: true,
          },
        },
        row: {
          colors: undefined,
          opacity: 0.1,
        },
        column: {
          colors: undefined,
          opacity: 0.1,
        },
        padding: {
          top: 0,
          right: 0,
          bottom: 0,
          left: 0,
        },
      },
      colors: ["#fff", "#000"],
      chart: {
        toolbar: {
          show: false,
        },
        //background: '#fff',
        foreColor: "#fff",
        height: 500,
        type: "bar",
      },
      plotOptions: {
        bar: {
          dataLabels: {
            position: "top", // top, center, bottom
          },
        },
      },
      dataLabels: {
        enabled: true,
        formatter: function () {
          return "";
        },
        offsetY: -10,
        style: {
          fontSize: "12px",
          colors: ["#304758"],
        },
      },

      xaxis: {
        categories: Array.from(Array(61).keys()),
        position: "bottom",
        title: {
          style: {
            color: "#fff",
          },
          text: "Fiber Diameter (um)",
          offsetY: 10,
        },
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: true,
        },
        crosshairs: {
          fill: {
            type: "gradient",
            gradient: {
              colorFrom: "#D8E3F0",
              colorTo: "#BED1E6",
              stops: [0, 100],
              opacityFrom: 0.4,
              opacityTo: 0.5,
            },
          },
        },
        tooltip: {
          enabled: true,
        },
      },
      yaxis: {
        title: {
          style: {
            color: "#fff",
          },
          text: "Fiber Count",
          offsetX: 10,
        },
        min: 0,
        axisBorder: {
          show: true,
        },
        axisTicks: {
          show: true,
        },
        labels: {
          show: true,
          formatter: function (val) {
            return Math.round(val);
          },
        },
      },
      title: {
        text: "Fiber Diameter (microns)",
        floating: false,
        offsetY: 10,
        align: "center",
        style: {
          color: "#fff",
        },
      },
    },
    chartOptions2: {
      grid: {
        padding: {
          top: 0,
          right: 0,
          bottom: 20,
          left: 0,
        },
        show: true,
        borderColor: "#474747",
        strokeDashArray: 0,
        position: "back",
        xaxis: {
          lines: {
            show: false,
          },
        },
        yaxis: {
          lines: {
            show: true,
          },
        },
        row: {
          colors: undefined,
          opacity: 0.1,
        },
        column: {
          colors: undefined,
          opacity: 0.1,
        },
      },
      colors: ["#000"],
      chart: {
        toolbar: {
          show: false,
        },
        //background: '#fff',
        foreColor: "#000",
        height: 500,
        type: "bar",
      },
      plotOptions: {
        bar: {
          dataLabels: {
            position: "top", // top, center, bottom
          },
        },
      },
      dataLabels: {
        enabled: true,
        formatter: function () {
          return "";
        },
        offsetY: -10,
        style: {
          fontSize: "12px",
          colors: ["#304758"],
        },
      },

      xaxis: {
        labels: {
          offsetY: -3,
          show: true,
        },
        categories: Array.from(Array(61).keys()),
        position: "bottom",
        title: {
          style: {
            color: "#000",
          },
          text: "Fiber Diameter (um)",
          offsetY: 30,
        },
        axisBorder: {
          offsetY: -5,
          show: false,
        },
        axisTicks: {
          show: true,
          color: "#000",
        },
        crosshairs: {
          fill: {
            type: "gradient",
            gradient: {
              colorFrom: "#D8E3F0",
              colorTo: "#BED1E6",
              stops: [0, 100],
              opacityFrom: 0.4,
              opacityTo: 0.5,
            },
          },
        },
        tooltip: {
          enabled: true,
        },
      },
      yaxis: {
        title: {
          style: {
            color: "#000",
          },
          text: "Fiber Count",
          offsetX: -200,
          offsetY: -260,
        },
        min: 0,
        axisBorder: {
          show: true,
          offsetY: 0,
        },
        axisTicks: {
          show: true,
        },
        labels: {
          offsetX: 30,
          show: true,
          formatter: function (val) {
            return Math.round(val);
          },
        },
      },
      title: {
        text: "Fiber Diameter (microns)",
        floating: false,
        offsetY: 10,
        align: "center",
        style: {
          color: "#000",
        },
      },
    },
  }),
  watch: {
    storeDocuments() {
      if (this.storeDocuments.length) {
        this.results = this.storeDocuments.filter(
          (d) => d.sampleID === this.$route.params.sampleid
        );
        this.series[0].data = this.getDataArr(
          this.results[0]["minDiameter"],
          this.results[0]["maxDiameter"],
          this.results[0]["fibersHist"]
        );
        this.isLoading = false;
        this.displayResults = true;
      }
    },
  },
  created() {
    this.isLoading = true;
    if (this.storeDocuments.length) {
      this.results = this.storeDocuments.filter(
        (d) => d.sampleID === this.$route.params.sampleid
      );
      this.series[0].data = this.getDataArr(
        this.results[0]["minDiameter"],
        this.results[0]["maxDiameter"],
        this.results[0]["fibersHist"]
      );
      this.isLoading = false;
      this.displayResults = true;
    } else {
      this.$store.dispatch("populateDocuments");
    }
  },
  computed: {
    storeDocuments() {
      return this.$store.state.documents ? this.$store.state.documents : [];
    },
  },
  methods: {
    generateReport() {
      this.$refs.html2Pdf.generatePdf();
    },
    getDataArr(min, max, dataSt) {
      var dataStr = dataSt.replace(/[[]']+/g, "");
      var vals = dataStr.split(",");
      0;
      var i = 0;
      var retArr = [];
      var j = 0;
      for (
        i = this.results[0].minDiameter - 5;
        i < this.results[0].maxDiameter + 5;
        ++i
      ) {
        if (i >= min && i <= max) {
          retArr.push({ x: i, y: parseInt(vals[j]) });
          j++;
        } else {
          retArr.push({ x: i, y: 0 });
        }
      }
      return retArr;
    },
  },
};
</script>
<style scoped>
</style>