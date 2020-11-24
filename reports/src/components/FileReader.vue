<template>
    <label class = "text-reader">
        <input type = "file" @change="loadTxtFromFile">
    </label>
</template>

<script>
import FileReader from "./FileReader";
export default {
    methods: {
        loadTextFromFile(ev){
            const file = ev.target.files[0];
            const reader = new FileReader();

            reader.onload = e =>this.$emit("load",etarget.result);
            reader.readAsText(file);
        }
    },
    components: {
        FileReader
    }
};
</script>