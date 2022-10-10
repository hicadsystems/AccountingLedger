<template>
    <!-- WRAPPER -->
    <div>

        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">�</span>
            </button>
        </div>
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">

            <div class="row">
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Student Number</label>
                        <vuejsAutocomplete source="/api/StudentRecord/getAllStudentByNameLimited/"
                                        input-class="form-control"
                                        @selected="setValueStudent"
                                        v-model="pp">
                        </vuejsAutocomplete>

                    </div>
                </div>
            </div>
        <div v-if="claimlist" class="card-body">     
        <div  v-show="wantshow">
            <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-label">Registration Number</label>
                    <input class="form-control" name="Reg_Number" v-model="postBody.Reg_Number" readonly />
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-label">Student Name</label>
                    <input class="form-control" name="studentname" v-model="postBody.studentname" readonly />
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-label">Amount</label>
                    <input class="form-control" name="Amount" v-model="postBody.Amount" readonly />
                </div>
            </div>
            </div>
            <!-- <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-label">Voucher Number</label>
                    <input class="form-control" name="VoucherNumber" v-model="postBody.VoucherNumber" />
                </div>
            </div>


            </div> -->

            <div class="row">
                <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group">
                    <button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button>
                </div>
            </div>
            </div>
        </div>

            </div>

        </div>

    <!-- END WRAPPER -->
</template>

<script>
    import vuejsAutocomplete from 'vuejs-auto-complete'
export default {
     components: {

         vuejsAutocomplete
        },
    data() {

        return {
        responseMessage:'',
            errors: null,
            searchData: '',
            submitorUpdate: 'submit',
            autoselectenabled: false,
            canProcess: true,
            claimlist:null,
            wantshow:true,
            wantshow2:true,
            wantshow3:false,
            claim:null,
            textlimit:10,
            acctno:"",
            maxLengthInCars: 10,
            pp:'',
        postBody: {
            amountPaid: 0,
            Amount: 0,
            studentname:'',
            VoucherNumber:'',
            Reg_Number:''
  
        },
        };
        },
            
     methods: {

         setValueStudent(result) {
             axios.get(`/api/StudentClaim/GetStudentClaim/${result.value}`)
                        .then(response => 
                        {
                            this.claimlist=response.data
                            this.postBody.Amount=response.data.val.amount
                            this.postBody.studentname=response.data.val.createdBy
                            // this.postBody.Transdate=response.data.val.transdate
                            this.postBody.Reg_Number=response.data.val.reg_Number
                           
                        })    
                                           
           },
         checkForm: function (e) {
             if (this.postBody.Amount) {
              e.preventDefault();
              this.canProcess = false;
              
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('Required');
          }
        },
        postPost() {
                alert('i am here 5')
                    axios.post(`/api/StudentClaim/UpdateCLaim`, this.postBody)
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postBody.studentname = ''; this.postBody.Amount = '';
                                this.wanttoupdate = true;

                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
            }
        },
     }

</script>