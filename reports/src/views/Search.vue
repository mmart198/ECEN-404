<template>
  <div>
    <v-content>
      <div v-if="!storeDocuments.length" style="width: 100%;min-height: 700px; display: flex; justify-content: center; align-items: center;">
        <div class="lds-ellipsis centered-two" style="width: fit-content;background: red;">
          <div></div>
          <div></div>
          <div></div>
          <div></div>
        </div>
      </div>
      <div v-else style="margin-top: 40px;">
        <div class="centered" style="width: 98%">
          <v-text-field
            class="slide-in-top"
            style="width: 50%; min-width: 470px;"
            v-if="!displayResults && !isLoading"
            label="Solo"
            placeholder="Search for a document"
            solo
            v-model="query"
          ></v-text-field>
        </div>

        <v-data-table
          dark
          :headers="headers"
          :items="results"
          :items-per-page="5"
          class="elevation-1 slide-in-top centered"
          style="width: 98%"
          @click:row="viewDocument"
        ></v-data-table>
      </div>
    </v-content>
    <v-footer color="#9e062a" app>
      <span class="white--text">ECEN 403</span>
    </v-footer>
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
Vue.use(VueAxios, axios);

export default {
  props: {
    source: String
  },

  data: () => ({
    query: "",
    displayResults: false,
    isLoading: false,
    results: [],
    headers: [
      {
        text: "Sample ID",
        align: "start",
        sortable: true,
        value: "sampleID"
      },
      {
        text: "Sample Log",
        align: "start",
        sortable: true,
        value: "sampleLog"
      },
      {
        text: "Sample Name",
        align: "start",
        sortable: true,
        value: "sampleName"
      },
      {
        text: "Date/Time",
        align: "start",
        sortable: true,
        value: "date_Time"
      },
      {
        text: "Company Name",
        align: "start",
        sortable: true,
        value: "companyName"
      },
      {
        text: "Customer Name",
        align: "start",
        sortable: true,
        value: "customerName"
      },
      {
        text: "Customer ID",
        align: "start",
        sortable: true,
        value: "customerID"
      },
      {
        text: "Mean Diameter",
        align: "start",
        sortable: true,
        value: "meanDiameter"
      },
      {
        text: "Comfort Factor",
        align: "start",
        sortable: true,
        value: "comfortFactor"
      },
      {
        text: "Total Fiber Count",
        align: "start",
        sortable: true,
        value: "totalFiberCount"
      },
      {
        text: "STD",
        align: "start",
        sortable: true,
        value: "std"
      },
      {
        text: "CEN",
        align: "start",
        sortable: true,
        value: "cem"
      },
      {
        text: "Min Staple",
        align: "start",
        sortable: true,
        value: "minStaple"
      },
      {
        text: "Max Staple",
        align: "start",
        sortable: true,
        value: "maxStaple"
      },
      {
        text: "Min Diameter",
        align: "start",
        sortable: true,
        value: "minDiameter"
      },
      {
        text: "Max Diameter",
        align: "start",
        sortable: true,
        value: "maxDiameter"
      },
      {
        text: "Mean Curvature",
        align: "start",
        sortable: true,
        value: "meanCurvature"
      },
      {
        text: "SD Curvature",
        align: "start",
        sortable: true,
        value: "sdCurvature"
      },
      {
        text: "Staple Length",
        align: "start",
        sortable: true,
        value: "stapleLength"
      }
    ]
  }),
  watch: {
    query() {
      this.search(this.query);
    },
    storeDocuments() {
      this.results = this.storeDocuments;
    }
  },
  created () {
    if (this.storeDocuments.length) {
      this.results = this.storeDocuments;
    }
  },
  computed: {
    storeDocuments() {
      return this.$store.state.documents;
    }
  },
  methods: {
    viewDocument(value) {
      this.$router.push(`/results/${value.sampleID}`);
    },
    search(query) {
      let results = [];
      this.storeDocuments.forEach(d =>
        JSON.stringify(d)
          .toLowerCase()
          .includes(query.toLowerCase())
          ? results.push(d)
          : null
      );
      this.results = results;
    },
    getDataArr(min, max, dataSt) {
      var dataStr = dataSt.replace(/[[]']+/g, "");
      var vals = dataStr.split(",");
      0;
      var i = 0;
      var retArr = [];
      var j = 0;
      for (i = 1; i <= 60; ++i) {
        if (i >= min && i <= max) {
          retArr.push(vals[j]);
          ++j;
        } else {
          retArr.push(0);
        }
      }
      return retArr;
    }
  }
};
</script>
<style scoped>
.flex-box {
  display: flex;
  justify-content: center;
  align-content: center;
}
.centered-two {
  margin: auto;
}
.centered {
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 70%;
}
</style>