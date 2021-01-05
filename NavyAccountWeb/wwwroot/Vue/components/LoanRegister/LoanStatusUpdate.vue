<template>
    <div>
            <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-success"> {{ responseMessage }}</div>
        <div class="row">
            <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                           input-class="form-control"
                                         
                                           v-model="postBody.PersonID">
                        </vuejsAutocomplete>

                    </div>
                </div>
        </div>
        <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-label">Batch No</label>
              <select class="form-control" v-model="postBody.batchNo" name="batchNo">
                  <option v-for="batch in batchlist" v-bind:value="batch.batchNo" v-bind:key="batch.batchNo">{{batch.batchNo}}</option>
              </select>
            </div>
        </div>
         
        <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-label">Status</label>

                        <select class="form-control" v-model="postBody.description" name="description" required>
                            <option v-for="loantype in loantypeList" v-bind:value="loantype.description" v-bind:key="loantype.id"> {{ loantype.description }} </option>
                        </select>

                    </div>
                </div>
         <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="form-label">Date</label>
                                        <vuejsDatepicker v-model="postBody.ApproveDate" input-class="form-control" name="approveDate"></vuejsDatepicker>
                                    </div>
                                </div>
        </div>
         <div class="col-12 ">
            <div class="btn-group mr-2 sw-btn-group-extra" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="button">{{submitorUpdate}}</button></div>
        </div>
    </div>
</template>
<script>
 import vuejsAutocomplete from 'vuejs-auto-complete'
  import vuejsDatepicker from 'vuejs-datepicker'
export default {
    components:{
        vuejsAutocomplete,
        vuejsDatepicker
    },
  
    data(){
        return{
            responseMessage:'',
            errors:'',
            submitorUpdate : 'Submit',
            searchdata:'',
            batchlist:null,
            loantypeList:null,

        postBody:{
            PersonID:0,
            LoanTypeID:null,
            batchNo:'',
            approveDate:'',
            description:''
        }

        };
    },
    mounted() {
            this.$store.state.objectToUpdate = null,
             this.wanttoupdate = true,
        axios
            .get('/Api/LoanRegister/getloanStatus')
            .then(response => (this.loantypeList = response.data))
   

         axios
        .get('/Api/LoanRegister/getloanbyBatch2')
        .then(response=>(this.batchlist=response.data))


      
     },
     
    watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) { 
         this.postBody.status = this.$store.state.objectToUpdate.status,
         this.postBody.ApproveDate = this.$store.state.objectToUpdate.approveDate,
         this.postBody.batchNo = this.$store.state.objectToUpdate.batchNo,
         this.postBody.PersonID = this.$store.state.objectToUpdate.personID

                this.submitorUpdate = 'Update';
               
        }
    },
  
    methods:{
        
    checkForm: function (e) {
              
                    this.postPost();
            },
      postPost() {
       alert('Do you want to update this Loan?');
                axios.put(`/api/LoanRegister/UpdateLoanStatus`, this.postBody)
                    .then(response => {
                        this.responseMessage = response.data.responseDescription; this.canProcess = true;
                        if (response.data.responseCode == '200') {
                            this.submitorUpdate = 'Submit'
                            this.postBody.description = null; this.postBody.PersonID = null;
                            this.postBody.batchNo = ''; this.postBody.reversalDate = null;

                        }
                    })
                    .catch(e => {
                        this.errors.push(e)
                    });

            }   
 }
 
    
};
</script>