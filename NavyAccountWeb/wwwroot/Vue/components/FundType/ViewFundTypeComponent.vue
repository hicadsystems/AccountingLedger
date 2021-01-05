<template>
    	<!-- WRAPPER -->
    <div>
        
        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Code</th>
                        <th>Description</th>
                        <th>Start Date</th>
                        <th>Processing Month</th>
                        <th>Processing Year</th>

                       
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="FundType in fundtypeList">
                        <td>{{ FundType.code }}</td>
                        <td>{{ FundType.description }}</td>
                        <td>{{ FundType.startDate }}</td>
                        <td>{{ FundType.processingMonth }}</td>
                        <td>{{ FundType.processingYear }}</td>

                        <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(FundType)">Edit</button></td>
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processDelete(FundType.id)">Delete</button></td>
                    </tr>
                </tbody>
              
            </table>
    </div>

    </div>

        <!-- END WRAPPER -->
</template>

<script>
export default { 
    
            data() {
                return {
                fundtypeList:null
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },
         watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
            this.getallfund();
        }
    },
    mounted () {
        this.getallfund()
     },
     methods: {
 
        processRetrieve : function (FundType) {
            this.$store.state.objectToUpdate = FundType;
         },
         getallfund: function () {
             axios
            .get('/api/FundType/getAllFundTypes')
            .then(response => (this.fundtypeList = response.data))
         },
          processDelete: function (id) {

             axios.get(`/api/FundType/RemoveFundType/${id}`)
                 .then(response => {
                     if (response.data.responseCode == '200') {
                         alert("fund type successfully deleted");
                         this.getallfund();
                     }
                 }).catch(e => {
                            this.errors.push(e)
                        });

         }
    }
    
};
</script>